using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public EnemyManager.EnemyType type;

    public Path currentPath;
    public bool timing = false;

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

        //Debug.Log(speed * Time.deltaTime);
        if (currentPath.WithinDistance(transform.position, currentWaypoint))
        {
            currentWaypoint = currentPath.NextWaypoint(currentWaypoint);
        }

        if (currentWaypoint < 0)
        {
            currentPath = null;
            EnemyManager.instance.ResetPosition(this);
        }
    }

}
