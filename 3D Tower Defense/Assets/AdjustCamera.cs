using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class AdjustCamera : MonoBehaviour {

    public float rotSpeed = 1.5f;
    public float zoomSpeed = 0.25f;
    public float expectedFPS = 30f;

    [SerializeField]
    private Transform cameraParent;

    // Vars for zooming camera
    [SerializeField]
    private float stopZoomingDist = 15f;

    // Use this for initialization
    void Start ()
    {
        if (!cameraParent)
            throw new System.NullReferenceException("No main parent for camera adjuster to manipulate camera");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount == 1)
        {
            Rotate();
        } else if(Input.touchCount == 2)
        {
            Zoom();
        } 
	}

    private void Zoom()
    {
        //// Calculate distance between touches 
        float curDist = Vector3.SqrMagnitude(Input.touches[0].position - Input.touches[1].position);
        float prevDist = Vector3.SqrMagnitude(Input.touches[0].deltaPosition - Input.touches[1].deltaPosition);

        //// Determine which way too zoom
        //Vector3 zoomDir = curDist > previousDist ? mainCamera.transform.forward : -mainCamera.transform.forward;
        //zoomDir = mainCamera.transform.InverseTransformDirection(zoomDir);

        //float dst = Vector3.SqrMagnitude(mainCamera.transform.position - Vector3.zero);

        //if (dst > stopZoomingDist || zoomDir == -mainCamera.transform.forward)
        //{
        //    // Move Camera in direction
        //    mainCamera.transform.Translate(zoomDir * zoomSpeed * Time.deltaTime * expectedFPS);
        //}

        //previousDist = curDist;
    }
    
    private void Rotate()
    {
        Vector3 curDist = Input.touches[0].position;
        Vector3 prevPos = Input.touches[0].deltaPosition;

        if(curDist != prevPos)
        {
            Debug.Log("hg");
            Vector3 rotDir = (curDist - prevPos);
            cameraParent.transform.Rotate(rotDir.y, rotDir.x, 0, Space.Self);
        }
    }
}
public static class ExtensionMethods
{
    //public static Vector3 RotateAroundLocal(this Vector3 Point, Vector3 Direction, )

    //Returns the rotated Vector3 using a Quaterion
    public static Vector3 RotateAroundPivot(this Vector3 Point, Vector3 Pivot, Quaternion Angle)
    {
        return Angle * (Point - Pivot) + Pivot;
    }
    //Returns the rotated Vector3 using Euler
    public static Vector3 RotateAroundPivot(this Vector3 Point, Vector3 Pivot, Vector3 Euler)
    {
        return RotateAroundPivot(Point, Pivot, Quaternion.Euler(Euler));
    }
    //Rotates the Transform's position using a Quaterion
    public static void RotateAroundPivot(this Transform Me, Vector3 Pivot, Quaternion Angle)
    {
        Me.position = Me.position.RotateAroundPivot(Pivot, Angle);
    }
    //Rotates the Transform's position using Euler
    public static void RotateAroundPivot(this Transform Me, Vector3 Pivot, Vector3 Euler)
    {
        Me.position = Me.position.RotateAroundPivot(Pivot, Quaternion.Euler(Euler));
    }
}