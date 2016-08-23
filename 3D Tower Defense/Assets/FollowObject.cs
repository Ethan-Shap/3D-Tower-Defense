using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public GameObject objToFollow;

    public bool offset = true;
    public bool rotateWithObj = false;

	// Use this for initialization
	void Start ()
    {
        if (!objToFollow)
            throw new System.NullReferenceException("No Object to Follow");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
