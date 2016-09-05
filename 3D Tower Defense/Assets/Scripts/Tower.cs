using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tower : MonoBehaviour {

    // Tower Attributes
    public string title;
    public int damage;
    public float range;

    private Animator animator;

    private static Enemy[] enemies;
    private Enemy currentEnemy = null;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        TargetNearestEnemy();
	}

    public void TriggerBuildAnimation()
    {
        animator.SetTrigger("BuildTrigger");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.4f);
    }

    private void TargetNearestEnemy()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>();
        float closestDistance = Mathf.Infinity;
        Enemy closestEnemy = null;
        foreach(Enemy enemy in enemies)
        {
            float distance = Vector3.SqrMagnitude(transform.position - enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if(closestDistance <= range)
        {
            currentEnemy = closestEnemy;
        }
    }

}
