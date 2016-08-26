using UnityEngine;
using System.Collections;

public class LockTransform : MonoBehaviour {

    public bool lockPosition = false;
    public bool lockPosX = false;
    public bool lockPosY = false;
    public bool lockPosZ = false;

    public bool lockRotation = false;
    public bool lockRotX = false;
    public bool lockRotY = false;
    public bool lockRotZ = false;

    // Defaults
    Quaternion defaultRot;
    Vector3 defaultPos;
	
    private void Awake()
    {
        defaultRot = transform.rotation;
        defaultPos = transform.position;
    }

	// Update is called once per frame
	void Update ()
    {
        // Lock Positions
        if (lockPosition)
            transform.position = defaultPos;
        if (lockPosX)
            transform.position.Set(defaultPos.x, transform.position.y, transform.position.z);
        if (lockPosY)
            transform.position.Set(transform.position.x, defaultPos.y, transform.position.z);
        if(lockPosZ)
            transform.position.Set(transform.position.x, transform.position.y, defaultPos.z);

        if (lockRotation)
            transform.rotation = defaultRot;
        if(lockRotX)
            transform.rotation 
    }
}
