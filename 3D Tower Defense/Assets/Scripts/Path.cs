using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    public Transform[] waypoints;

    private float withinDist = 0.2f;

    private void Awake()
    {
        Transform[] tempWaypoints = GetComponentsInChildren<Transform>();
        waypoints = new Transform[tempWaypoints.Length - 1];
        for (int i = 1; i < tempWaypoints.Length; i++)
            waypoints[i - 1] = tempWaypoints[i];

    }

    public int NextWaypoint(int currentWaypoint)
    {
        if (currentWaypoint + 1 < waypoints.Length)
            return currentWaypoint + 1;

        return -1;
    }

    public Transform GetWaypointPosition(int currentWaypoint)
    {
        if(currentWaypoint >= 0 && currentWaypoint < waypoints.Length)
            return waypoints[currentWaypoint];

        return null;
    }

    public bool WithinDistance(Vector3 pos, int currentWaypoint)
    {
        float dist = Vector3.Distance(pos, waypoints[currentWaypoint].position);
        if(dist < withinDist)
        {
            return true;
        }
        return false;
    }

    public float TotalDistance()
    {
        float totalDist = 0;
        for(int i = 1; i < waypoints.Length; i++)
        {
            totalDist += Vector3.Distance(waypoints[i-1].position, waypoints[i].position);
        }
        return totalDist;
    }

}