using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	public LaunchArcMesh arcMesh;

	public Transform starter;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		arcMesh.angle =starter.transform.eulerAngles.y;
		if( Input.GetMouseButton( 0 ) )
		{
			
		}
	}
}
