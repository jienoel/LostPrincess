using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchArcMesh : MonoBehaviour
{
	private Mesh mesh;

	public float meshWidth;
	public float velocity;
	public float angle;
	public int resolution = 10;
	private float g;
	private float radianAngle;

	void Awake()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		g = Mathf.Abs( Physics2D.gravity.y );
	}

	void OnValidate()
	{
		if( mesh != null && Application.isPlaying )
		{
			Show();
		}
	}

	// Use this for initialization
	void Start ()
	{
		Show();
	}

	public void Show()
	{
		MakeArcMesh(CalculateArcArray());
	}

	void MakeArcMesh(Vector3[] arcVerts)
	{
		mesh.Clear();
		Vector3[] vertices = new Vector3[(resolution + 1) * 2] ;
		int[] triangles = new int[resolution * 6 * 2];
		for( int i = 0; i <= resolution; i++ )
		{
			// set verticles
			vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
			vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);
			
			//set trianlges
			if( i != resolution )
			{
				triangles[i * 12] = i * 2;
				triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
				triangles[i * 12 + 2] = triangles[i * 12 + 3] =(i + 1) * 2 ;
				triangles[i * 12 + 5] = (i + 1) * 2 + 1;
				
				triangles[i * 12 + 6] = i * 2;
				triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
				triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
				triangles[i * 12 + 11] = (i + 1) * 2 + 1;

			}

		}
		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}

	Vector3[] CalculateArcArray()
	{
		Vector3[] arcArrary = new Vector3[resolution+1];
		radianAngle = Mathf.Deg2Rad * angle;
		float maxDistance = (velocity * velocity * Mathf.Sin( 2 * radianAngle )) / g;
		for( int i = 0; i <= resolution; i++ )
		{
			float t = (float)i / (float)resolution;
			arcArrary[i] = CalculateArcPoint( t, maxDistance );
		}
		return arcArrary;
	}

	Vector3 CalculateArcPoint( float t, float maxDistance )
	{
		float x = t * maxDistance;
		float y = x * Mathf.Tan( radianAngle ) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos( radianAngle ) * Mathf.Cos( radianAngle )));
		return new Vector3(x, y);
	}
}
