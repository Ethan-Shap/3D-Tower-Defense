using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

    public enum ControlType { Regular, Previewing }
    public static ControlType currentControl;

    public float rotSpeed = 1.5f;
    public float zoomSpeed = 0.25f;
    public float expectedFPS = 30f;

    public Transform cameraFocusPoint;

    [SerializeField]
    private Transform cameraParent;
    private Camera c;

    // Vars for zooming camera
    [SerializeField]
    private float stopZoomingDist = 15f;
    private float prevDist = 0;
    private float minDist = 8f;
    private float maxDist = 14f;

    private Vector2 prevPos;
    private float minXRot = 3, maxXRot = 45;
    private Previewer previewer;

    private bool collidedWithTouchInput;

    // Use this for initialization
    void Start ()
    {
        if (!cameraParent)
            throw new System.NullReferenceException("No main parent for camera adjuster to manipulate camera");

        if (!cameraFocusPoint)
            throw new System.NullReferenceException("No camera focus point");

        c = cameraParent.GetComponentInChildren<Camera>();
        c.transform.LookAt(cameraFocusPoint);

        previewer = Previewer.instance;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentControl == ControlType.Regular)
        {
            // Camera movment when there is no tower preview
            if (Input.touchCount == 1)
            {
                Rotate();
                cameraParent.transform.eulerAngles = new Vector3(cameraParent.transform.eulerAngles.x, cameraParent.transform.eulerAngles.y, 0);
            }
            else if (Input.touchCount == 2)
            {
                Zoom();
            }
        } else if (currentControl == ControlType.Previewing)
        {
            // Camera Movement if there's a tower being previewed
            if (Input.touchCount == 1 && !IsPointerOverUIObject())
            {
                previewer.MovePreview(Input.GetTouch(0).position);
            } else if (Input.touchCount == 2)
            {
                Rotate();
                cameraParent.transform.eulerAngles = new Vector3(cameraParent.transform.eulerAngles.x, cameraParent.transform.eulerAngles.y, 0);
            }
        }
	}

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.GetTouch(0).position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log(result.gameObject.name);
        }

        return results.Count > 1;
    }

    private void Zoom()
    {
        // Calculate distance between touches 
        float curDist = Vector3.SqrMagnitude(Input.touches[0].position - Input.touches[1].position);

        // Determine which way too zoom
        Vector3 zoomDir = curDist > prevDist ? c.transform.forward : -c.transform.forward;
        zoomDir = c.transform.InverseTransformDirection(zoomDir);

        // Move Camera in direction
        c.transform.Translate(zoomDir * zoomSpeed * Time.deltaTime * expectedFPS);

        // Clamp Camera Position
        c.transform.position = c.transform.position.ClampMagnitude(cameraParent.transform.position, minDist, maxDist);
        c.transform.LookAt(cameraParent);

        prevDist = curDist;
    }

    private void Rotate()
    {
        // calculates the rotation based on which way the user moved 
        Vector3 rotDir = (Input.GetTouch(0).position - prevPos) * -1;

        // Using touch to move, reverse the x and y
        rotDir.Set(rotDir.y, rotDir.x, 0);

        // rotates the camera in a direction at a speed in local space
        cameraParent.transform.Rotate(rotDir, Time.deltaTime * rotSpeed * expectedFPS, Space.Self);

        float newXRot = Mathf.Clamp(cameraParent.transform.eulerAngles.x, minXRot, maxXRot);

        cameraParent.transform.rotation = Quaternion.Euler(newXRot, cameraParent.transform.localEulerAngles.y, cameraParent.transform.localEulerAngles.z);

        // sets previous mouse position to first touch
        prevPos = Input.GetTouch(0).position;
    }
}
