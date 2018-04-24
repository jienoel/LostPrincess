using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTest : MonoBehaviour
{

 private SpriteRenderer spriteR;
    private Rect buttonPos1;
    private Rect buttonPos2;

    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        buttonPos1 = new Rect(10.0f, 10.0f, 200.0f, 30.0f);
        buttonPos2 = new Rect(10.0f, 50.0f, 200.0f, 30.0f);
    }

    void OnGUI()
    {
        if (GUI.Button(buttonPos1, "Draw Debug"))
            DrawDebug();

        if (GUI.Button(buttonPos2, "Perform OverrideGeometry"))
	        ChangeCircle();
    }

    // Show the sprite triangles
    void DrawDebug()
    {
        Sprite sprite = spriteR.sprite;

        ushort[]  t = sprite.triangles;
        Vector2[] v = sprite.vertices;
        int a, b, c;

        // draw the triangles using grabbed vertices
        for (int i = 0; i < t.Length; i = i + 3)
        {
            a = t[i];
            b = t[i + 1];
            c = t[i + 2];
            Debug.DrawLine(v[a], v[b], Color.white, 100.0f);
            Debug.DrawLine(v[b], v[c], Color.white, 100.0f);
            Debug.DrawLine(v[c], v[a], Color.white, 100.0f);
        }
    }

    // Edit the vertices obtained from the sprite.  Use OverrideGeometry to
    // submit the changes.
    void ChangeSprite()
    {
        Sprite o = spriteR.sprite;
        Vector2[] sv = o.vertices;

        for (int i = 0; i < sv.Length; i++)
        {
            sv[i].x = Mathf.Clamp(
                    (o.vertices[i].x - o.bounds.center.x -
                     (o.textureRectOffset.x / o.texture.width) + o.bounds.extents.x) /
                    (2.0f * o.bounds.extents.x) * o.rect.width,
                    0.0f, o.rect.width);

            sv[i].y = Mathf.Clamp(
                    (o.vertices[i].y - o.bounds.center.y -
                     (o.textureRectOffset.y / o.texture.height) + o.bounds.extents.y) /
                    (2.0f * o.bounds.extents.y) * o.rect.height,
                    0.0f, o.rect.height);

            // make a small change to the left-most vertex
            if (i == 2)
                sv[i].x = sv[i].x - 50;
        }

        o.OverrideGeometry(sv, o.triangles);
    }

	private const int CircleSegmentCount = 64;
	private const int CircleVertexCount = CircleSegmentCount + 2;
	private const int CircleIndexCount = CircleSegmentCount * 3;

	void ChangeCircle()
	{
		
	}

	public Mesh MakeCircle(int numOfPoints)
	{
		float angleStep = 360.0f / (float)numOfPoints;
		List<Vector3> vertexList = new List<Vector3>();
		List<int> triangleList = new List<int>();
		Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angleStep);
		// Make first triangle.
		vertexList.Add(new Vector3(0.0f, 0.0f, 0.0f));  // 1. Circle center.
		vertexList.Add(new Vector3(0.0f, 0.5f, 0.0f));  // 2. First vertex on circle outline (radius = 0.5f)
		vertexList.Add(quaternion * vertexList[1]);     // 3. First vertex on circle outline rotated by angle)
		// Add triangle indices.
		triangleList.Add(0);
		triangleList.Add(1);
		triangleList.Add(2);
		for (int i = 0; i < numOfPoints - 1; i++)
		{
			triangleList.Add(0);                      // Index of circle center.
			triangleList.Add(vertexList.Count - 1);
			triangleList.Add(vertexList.Count);
			vertexList.Add(quaternion * vertexList[vertexList.Count - 1]);
		}
		Mesh mesh = new Mesh();
		mesh.vertices = vertexList.ToArray();
		mesh.triangles = triangleList.ToArray();
		return mesh;
	}
	
	
	public static Mesh GenerateCircleMesh()
	{
		var circle = new Mesh();
		var vertices = new List<Vector3>(CircleVertexCount);
		var indices = new int[CircleIndexCount];
		var segmentWidth = Mathf.PI * 2f / CircleSegmentCount;
		var angle = 0f;
		vertices.Add(Vector3.zero);
		for (int i = 1; i < CircleVertexCount; ++i)
		{
			vertices.Add(new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)));
			angle -= segmentWidth;
			if (i > 1)
			{
				var j = (i - 2) * 3;
				indices[j + 0] = 0;
				indices[j + 1] = i - 1;
				indices[j + 2] = i;
			}
		}
		circle.SetVertices(vertices);
		circle.SetIndices(indices, MeshTopology.Triangles, 0);
		circle.RecalculateBounds();
		return circle;
	}
}
