using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class AdjustCamera : MonoBehaviour {

    public float rotSpeed = 1.5f;
    public float zoomSpeed = 0.25f;
    public float expectedFPS = 30f;

    [SerializeField]
    private Camera mainCamera;

    // Vars for Rotating Camera
    private Vector2 previousMousePos;
    private Vector3 rotDir;

    // Vars for zooming camera
    [SerializeField]
    private float previousDist;
    private float stopZoomingDist = 15f;

    // Use this for initialization
    void Start ()
    {
        if (!mainCamera)
            throw new System.NullReferenceException("No main camera for camera adjuster");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount == 1)
        {
            Rotate(Vector3.zero);
            mainCamera.transform.rotation = Quaternion.Euler(new Vector3(mainCamera.transform.rotation.eulerAngles.x, mainCamera.transform.rotation.eulerAngles.y, 0f));
        } else if(Input.touchCount == 2)
        {
            Zoom();
        } else
        {
            mainCamera.transform.rotation = Quaternion.Euler(new Vector3(mainCamera.transform.rotation.eulerAngles.x, mainCamera.transform.rotation.eulerAngles.y, 0f));
        }
	}

    private void Zoom()
    {
        // Calculate distance between touches 
        float curDist = Vector3.SqrMagnitude(Input.touches[0].position - Input.touches[1].position);

        // Determine which way too zoom
        Vector3 zoomDir = curDist > previousDist ? mainCamera.transform.forward : -mainCamera.transform.forward;
        zoomDir = mainCamera.transform.InverseTransformDirection(zoomDir);

        float dst = Vector3.SqrMagnitude(mainCamera.transform.position - Vector3.zero);

        if (dst > stopZoomingDist || zoomDir == -mainCamera.transform.forward)
        {
            // Move Camera in direction
            mainCamera.transform.Translate(zoomDir * zoomSpeed * Time.deltaTime * expectedFPS);
        }

        previousDist = curDist;
    }
    
    private void Rotate(Vector3 rotateAround)
    {
        // calculates the rotation based on which way the user moved 
        rotDir = (Input.GetTouch(0).position - previousMousePos) * -1;

        // Flip the rotation axis on x and y
        rotDir.Set(rotDir.y, rotDir.x, rotDir.z);
        rotDir = mainCamera.transform.InverseTransformDirection(rotDir);

        // rotates the camera about the desired point at an angle of rotation at rotation speed
        mainCamera.transform.RotateAround(rotateAround, rotDir, rotSpeed * Time.deltaTime * expectedFPS);

        // sets previous mouse position to first touch
        previousMousePos = Input.GetTouch(0).position;
    }
}
