using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    private Transform target;
    private float speed;
    [SerializeField]
    private int damage;

    private void Update()
    {
        if (target)
            MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

    public void SetProjectileVars(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    private void OnTriggerStay(Collider col)
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