using UnityEngine;

public class AdjustCamera : MonoBehaviour {

    public float rotSpeed = 1.5f;
    public float zoomSpeed = 0.25f;
    public float expectedFPS = 30f;

    [SerializeField]
    private Transform cameraParent;
    private Camera camera;

    // Vars for zooming camera
    [SerializeField]
    private float stopZoomingDist = 15f;
    private float prevDist = 0;

    private Vector2 prevPos;
    private float minXRot = 2, maxXRot = 45;

    // Use this for initialization
    void Start ()
    {
        if (!cameraParent)
            throw new System.NullReferenceException("No main parent for camera adjuster to manipulate camera");

        camera = cameraParent.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount == 1)
        {
            Rotate();
            cameraParent.transform.eulerAngles = new Vector3(cameraParent.transform.eulerAngles.x, cameraParent.transform.eulerAngles.y, 0);
        } else if(Input.touchCount == 2)
        {
            Zoom();
        } 
	}

    private void Zoom()
    {
        //// Calculate distance between touches 
        float curDist = Vector3.SqrMagnitude(Input.touches[0].position - Input.touches[1].position);

        // Determine which way too zoom
        Vector3 zoomDir = curDist > prevDist ? camera.transform.forward : -camera.transform.forward;
        zoomDir = camera.transform.InverseTransformDirection(zoomDir);

        // Calculate the distance between the camera and the cameras focal point
        float dst = Vector3.SqrMagnitude(camera.transform.position - cameraParent.transform.position);

        // Move Camera in direction
        camera.transform.Translate(zoomDir * zoomSpeed * Time.deltaTime * expectedFPS);

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
