using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class AdjustCamera : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public float rotSpeed = 2f;

    [SerializeField]
    private bool pointerDown = false;

    [SerializeField]
    private Camera mainCamera;

    private Vector2 previousMousePos;
    private Vector3 rotDir;

    // Use this for initialization
    void Start ()
    {
        if (!mainCamera)
            throw new System.NullReferenceException("No main camera for camera adjuster");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pointerDown)
        {
            RotateCamera(Vector3.zero);
        }
	}

    private void RotateCamera(Vector3 rotateAround)
    {
        // calculates the rotation based on which way the user moved 
        rotDir = (Input.GetTouch(0).position - previousMousePos) * -1;
        // Flip the rotation axis on x and y
        rotDir.Set(rotDir.y, rotDir.x, rotDir.z);

        // rotates the camera about the desired point at an angle of rotation at rotation speed
        mainCamera.transform.RotateAround(rotateAround, rotDir, rotSpeed);

        // sets previous mouse position to first touch
        previousMousePos = Input.GetTouch(0).position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        previousMousePos = eventData.position;
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }
}
