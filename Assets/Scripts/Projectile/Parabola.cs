using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parabola : MonoBehaviour {

    float objectT = 0; //timer for that object
 
    public Vector3 Ta, Tb; //transforms that mark the start and end
    public float h; //desired parabola height
 
    Vector3 a, b; //Vector positions for start and end
    private bool hasRun;
    private Transform obj;
    private float currCD;
    public float cd = 1;
    public Transform relative;
    private void Start()
    {
        obj = transform;
       // transform.position = Ta;
        currCD = 0;
    }

    void Update () {
        a = Ta; //Get vectors from the transforms
        b = Tb;
        transform.Translate ((Tb - Ta)*Time.deltaTime*1, relative);  
        if ( obj && currCD < cd ) {
            //Shows how to animate something following a parabola
            //objectT = Time.time % 1; //completes the parabola trip in one second
            //currCD += Time.deltaTime;
          //  objectT = currCD;
            //obj.position = SampleParabola( a, b, h, objectT );
        }
    }
 
 
    void OnDrawGizmos () {
        //Draw the height in the viewport, so i can make a better gif :]
        Handles.BeginGUI();
        GUI.skin.box.fontSize = 16;
        GUI.Box( new Rect( 10, 10, 100, 25 ), h + "" );
        Handles.EndGUI();
 
        //Draw the parabola by sample a few times
        Gizmos.color = Color.red;
        Gizmos.DrawLine( a, b );
        float count = 20;
        Vector3 lastP = a;
        for ( float i = 0; i < count + 1; i++ ) {
            Vector3 p = SampleParabola( a, b, h, i / count );
            Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;
            Gizmos.DrawLine( lastP, p );
            lastP = p;
        }
    }
 
    /// <summary>
    /// Get position from a parabola defined by start and end, height, and time
    /// </summary>
    /// <param name='start'>
    /// The start point of the parabola
    /// </param>
    /// <param name='end'>
    /// The end point of the parabola
    /// </param>
    /// <param name='height'>
    /// The height of the parabola at its maximum
    /// </param>
    /// <param name='t'>
    /// Normalized time (0->1)
    /// </param>S
    Vector3 SampleParabola ( Vector3 start, Vector3 end, float height, float t ) {
        if ( Mathf.Abs( start.y - end.y ) < 0.1f ) {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += Mathf.Sin( t * Mathf.PI ) * height;
            return result;
        } else {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirecteion = end - new Vector3( start.x, end.y, start.z );
            Vector3 right = Vector3.Cross( travelDirection, levelDirecteion );
            Vector3 up = Vector3.Cross( right, travelDirection );
            if ( end.y > start.y ) up = -up;
            Vector3 result = start + t * travelDirection;
            result += ( Mathf.Sin( t * Mathf.PI ) * height ) * up.normalized;
            return result;
        }
    }
}
