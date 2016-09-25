using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    private Transform target;
    private float speed;
    [SerializeField]
    private int damage;
    private ProjectileLauncher parentLauncher;
    private bool initialized = false;

    private void LateUpdate()
    {
        if (initialized)
        { 
            if (target && target.gameObject.activeInHierarchy)
                    MoveTowardsTarget();
            else
            {
                target = parentLauncher.ClosestEnemy();
                if (!target)
                    Destroy(gameObject);
            }
        }
    }

    private void MoveTowardsTarget()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

    public void SetProjectileVars(Transform target, float speed, int damage, ProjectileLauncher parent)
    {
        this.target = target;
        this.speed = speed;
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