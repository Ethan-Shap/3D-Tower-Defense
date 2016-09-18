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

    public Enemy GetCurrentEnemy()
    {
        if (currentEnemy != null)
            return currentEnemy;
        return null;
    }

    private void TargetNearestEnemy()
    {
        enemies = EnemyManager.instance.GetActiveEnemies();
        float closestDistance = Mathf.Infinity;
        Enemy closestEnemy = null;
        foreach(Enemy enemy in enemies)
        {
            float distance = Vector3.SqrMagnitude(transform.position - enemy.transform.position);
            if (distance < closestDistance * closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        //Debug.Log(closestDistance);
        //Debug.Log(range * range);

        if (closestDistance * closestDistance <= range * range)
        {
            Debug.Log("CLOSE");
            currentEnemy = closestEnemy;
        } else
        {
            currentEnemy = null;
        }
    }

}
