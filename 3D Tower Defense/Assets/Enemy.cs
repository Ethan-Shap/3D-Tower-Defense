using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public enum Type
    {
        White, Red, Blue, Grey, BOSS
    }

    public Path currentPath;

    [SerializeField]
    private float speed;
    private int currentWaypoint = 0;

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentPath)
        {
            MoveTowardsWaypoint();
        }
	}

    private void MoveTowardsWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPath.GetWaypointPosition(currentWaypoint).position, speed * Time.deltaTime);

        if(currentPath.WithinDistance(transform.position, currentWaypoint))
        {
            currentWaypoint = currentPath.NextWaypoint(currentWaypoint);
        }
    }

}
