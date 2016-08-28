using UnityEngine;

public class AdjustCamera : MonoBehaviour {

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

    // Use this for initialization
    void Start ()
    {
        if (!cameraParent)
            throw new System.NullReferenceException("No main parent for camera adjuster to manipulate camera");

        if (!cameraFocusPoint)
            throw new System.NullReferenceException("No camera focus point");

        c = cameraParent.GetComponentInChildren<Camera>();
        c.transform.LookAt(cameraFocusPoint);
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
public static class ExtensionMethods
{
    public static Vector3 Clamp(this Vector3 v, float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        return new Vector3(Mathf.Clamp(v.x, minX, maxX), Mathf.Clamp(v.y, minY, maxY), Mathf.Clamp(v.z, minZ, maxZ));
    }

    public static Vector3 ClampMagnitude(this Vector3 v, Vector3 point, float minDist, float maxDist)
    {
        float dist = Vector3.Magnitude(v - point);
        Vector3 dir = v - point; 
        dir = dir.normalized;
        if (dist <= minDist)
        {
            return (dir * minDist) + point;
        } else if (dist >= maxDist)
        {
            return (dir * maxDist) + point;
        } else
        {
            return v;
        }
    }
}