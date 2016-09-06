using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour {

    private List<Transform> waypoints;
    private float pathDistance;

	// Use this for initialization
	void Start ()
    {
        Transform[] tempWaypoints = GetComponentsInChildren<Transform>();
        for(int i = 0; i < tempWaypoints.Length; i++)
        {
            if(tempWaypoints[i].parent == this.transform.parent)
            {
                waypoints.Add(tempWaypoints[i]);
            }
        }

        foreach(Transform waypoint in waypoints)
        {

        }

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public float GetPathDistance()
    {

    }
}
