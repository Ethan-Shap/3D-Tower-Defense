using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public EnemyManager.EnemyType type;

    public Path currentPath;
    public bool timing = false;

    [SerializeField]
    private float speed;
    private float defaultSpeed;
    private int currentWaypoint = 0;
    private int damage = 1;

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

    public float DefaultSpeed
    {
        get
        {
            return defaultSpeed;
        }

        set
        {
            defaultSpeed = value;
        }
    }

    private void Start()
    {
        DefaultSpeed = Speed;
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
        if ((int)speed > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPath.GetWaypointPosition(currentWaypoint).position, speed * Time.deltaTime);

            if (currentPath.WithinDistance(transform.position, currentWaypoint))
            {
                currentWaypoint = currentPath.NextWaypoint(currentWaypoint);
            }

            if (currentWaypoint < 0)
            {
                currentWaypoint = 0;
                currentPath = null;
                Player.instance.Health -= damage;
                EnemyManager.instance.ResetPosition(this);
            }
        }
    }
}
