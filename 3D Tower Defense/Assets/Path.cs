using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    public Transform[] waypoints;

    private Transform currentWaypoint;
    private float withinDist = 0.2f;

    public Transform NextWaypoint()
    {
        if (currentWaypoint == null)
            currentWaypoint = waypoints[0];

        for(int i = 0; i < waypoints.Length; i++)
        {
            if (currentWaypoint == waypoints[i] && i + 1 <= waypoints.Length)
                return waypoints[i + 1];
        }
        return null;
    }

    public Transform CurrentWaypoint()
    {
        return currentWaypoint;
    }

    public bool WithinDistance(Vector3 pos)
    {
        float dist = Vector3.Distance(pos, currentWaypoint.position);
        if(dist < withinDist)
        {
            return true;
        }
        return false;
    }

}