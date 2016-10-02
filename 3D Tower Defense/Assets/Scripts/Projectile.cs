using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    private Transform target;
    private float speed;
    private bool canMove = true;
    [SerializeField]
    private int damage;
    private ProjectileLauncher parentLauncher;
    private bool initialized = false;

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

    public bool CanMove
    {
        get
        {
            return canMove;
        }

        set
        {
            canMove = value;
        }
    }

    private void LateUpdate()
    {
        if (initialized)
        {
            if (CanMove)
            {
                if (target && target.gameObject.activeInHierarchy)
                    MoveTowardsTarget();
                else
                {
                    target = parentLauncher.ClosestEnemy();
                    if (!target.gameObject.activeInHierarchy)
                        Destroy(gameObject);
                }
            }
        }
    }

    private void MoveTowardsTarget()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * Speed);
    }

    public void SetProjectileVars(Transform target, float speed, int damage, ProjectileLauncher parent)
    {
        this.target = target;
        this.Speed = speed;
        this.damage = damage;
        this.parentLauncher = parent;
        initialized = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            if (col.GetComponent<Health>())
            {
                col.GetComponent<Health>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}