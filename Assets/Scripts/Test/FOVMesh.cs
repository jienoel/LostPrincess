using UnityEngine;

public class FOVMesh : MonoBehaviour
{
    // 玩家
    public GameObject playerGameObject;

    // 光照半径
    public float range = 3;

    // 光照质量，数值越低，向四周辐射的射线越多，效果越好，但性能越低
    public int levelOfDetails = 1;
        
    // 接受光照产生阴影的对象
    public LayerMask[] layerMask;
        
        
    private Vector3 direction;
    private int index = 0;
    private int triIndex = 0;
    private int lod = 1;
    private float width;
    private GameObject go;
    private GameObject pGo;
    private Mesh mesh;
    private Vector3 worldPos;
        
    private Vector3[] verts;
    private int[] tris;
    private Vector2[] uvs;
    private GameObject didHit;

    private int mask;
        
        
    // Code that runs on entering the state.
    public void Start ()
    {
        go = gameObject;
        pGo = playerGameObject;
            
        mesh = new Mesh ();
        go.GetComponent<MeshFilter> ().mesh = mesh;
            
        lod = levelOfDetails;
        width = range;
            
        // 若loa为1，则共发射360条射线作为光线，则有360个顶点加个圆心
        verts = new Vector3[(360 / lod) + 1];
        // 每两个顶点跟圆心组成一个三角形，所以三角形的个数为定点数乘3
        tris = new int[(360 / lod) * 3];
            
        uvs = new Vector2[verts.Length];

        for (int i = 0; i < layerMask.Length; ++i) {
            mask |= layerMask [i];
        }
    }
        
    // Code that runs every frame.
    public void Update ()
    {
        index = 0;
        triIndex = 0;
            
        worldPos = pGo.transform.position;
            
        verts [index] = worldPos;
            
        index++;
            
        for (var a=0; a<360; a += lod) {
            var direction = new Vector3 (Mathf.Sin (Mathf.Deg2Rad * a), 0, Mathf.Cos (Mathf.Deg2Rad * a));
                
            direction = direction * width;
                
            RaycastHit hit;
                
            if (Physics.Raycast (worldPos, direction, out hit, width, mask)) {
                // 如果被射线探中，则将探测到的点作为网格的顶点
                verts [index] = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
                    
            } else {
                // 否则将射线的末端作为网格的顶点
                verts [index] = new Vector3 (direction.x + worldPos.x, worldPos.y, direction.z + worldPos.z);
            }
                
            index++;
        }
            
        // 根据网格顶点组合三角形
        for (var i=1; i<(360/lod); i++) {
            tris [triIndex] = 0;
            tris [triIndex + 1] = i;
            tris [triIndex + 2] = i + 1;
            triIndex += 3;
                
        }
            
        tris [((360 / lod) * 3) - 3] = 0;
        tris [((360 / lod) * 3) - 2] = 360 / lod;
        tris [((360 / lod) * 3) - 1] = 1;
            
        // 网格贴图
        int j = 0;
        while (j < uvs.Length) {
            uvs [j] = new Vector2 (verts [j].x, verts [j].z);
            j++;
        }
            
            
        // 重新组合网格
        mesh.Clear ();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals ();
    }
        
}