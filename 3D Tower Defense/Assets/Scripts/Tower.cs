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
    private Enemy currentEnemy;
    private static List<Enemy> sortedEnemies;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        sortedEnemies = new List<Enemy>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SortNearestEnemies();
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
        return currentEnemy;
    }

    public Enemy GetClosestEnemy()
    {
        return sortedEnemies[0];
    }

    private void SortNearestEnemies()
    {
        sortedEnemies = EnemyManager.instance.GetActiveEnemies().ToList();
        sortedEnemies.OrderBy(x => Vector3.SqrMagnitude(transform.position - x.transform.position));
        if(sortedEnemies.Count > 0)
        {
            if(Vector3.SqrMagnitude(transform.position - sortedEnemies[0].transform.position) <= range * range)
            {
                currentEnemy = sortedEnemies[0];
            } else
            {
                currentEnemy = null;
            }
        }

        //for(int i = 0; i < sortedEnemies.Count; i++)
        //{
        //    Debug.Log("i = " + i + " dist = " + Vector3.SqrMagnitude(transform.position - sortedEnemies[i].transform.position) + " name " + sortedEnemies[i].name);
        //}
    }
    //private void TargetNearestEnemy()
    //{
    //    enemies = EnemyManager.instance.GetActiveEnemies();
    //    float closestDistance = Mathf.Infinity;
    //    Enemy closestEnemy = null;
    //    foreach(Enemy enemy in enemies)
    //    {
    //        float distance = Vector3.SqrMagnitude(transform.position - enemy.transform.position);
    //        if (distance < closestDistance * closestDistance)
    //        {
    //            closestDistance = distance;
    //            closestEnemy = enemy;
    //        }
    //    }

    //    //Debug.Log(closestDistance);
    //    //Debug.Log(range * range);

    //    if (closestDistance * closestDistance <= range * range)
    //    {
    //        Debug.Log("CLOSE");
    //        currentEnemy = closestEnemy;
    //    } else
    //    {
    //        nextClosestEnemy = currentEnemy;
    //        currentEnemy = null;
    //    }
}

