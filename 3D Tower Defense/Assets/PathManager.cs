using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour {

    public static PathManager instance;

    private List<Path> paths;
    private float pathDistance;

    private void Awake()
    {
        instance = this;

        Transform[] tempWaypoints = GetComponentsInChildren<Transform>();

        for (int i = 0; i < tempWaypoints.Length; i++)
        {
            if (tempWaypoints[i].parent == this.transform)
            {
                Path newPath = new Path();
                paths.Add(newPath);
            }
        }

    }

	// Use this for initialization
	void Start ()
    {
        

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public float GetPathDistance()
    {
        for(int i = 0; i < waypoints.Count; i++)
        {
            if (i != 0)
                pathDistance += Vector3.Distance(waypoints[i-1].transform.position, waypoints[i].transform.position);
        }
        return pathDistance;
    }
}
