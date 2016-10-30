using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tower : MonoBehaviour {

    // Tower Attributes
    public string title;
    public int damage;
    public float range;
    public bool purchased = false;
    public TowerManager.TowerType type;

    private Animator animator;

    private static Enemy[] enemies;
    private Enemy currentEnemy;
    private List<Enemy> sortedEnemies;

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

    public void Pause()
    {
        if(type.ToString().Contains("single"))
        {
            GetComponent<ProjectileLauncher>().Pause();
        }
        else if (type.ToString().Contains("areaofeffect"))
        {
            GetComponent<AreaOfEffect>().Pause();
        }
    }

    public void Unpause()
    {
        if (type.ToString().Contains("single"))
        {
            GetComponent<ProjectileLauncher>().Unpause();
        }
        else if (type.ToString().Contains("areaofeffect"))
        {
            GetComponent<AreaOfEffect>().Unpause();
        }
    }

    public Enemy GetClosestEnemy()
    {
        return sortedEnemies.Count <= 0 ? null : sortedEnemies[0];
    }

    private void SortNearestEnemies()
    {
        sortedEnemies = EnemyManager.instance.GetActiveEnemies().ToList();
        sortedEnemies = sortedEnemies.OrderBy(x => Vector3.SqrMagnitude(transform.position - x.transform.position)).ToList();

        //for (int i = 0; i < sortedEnemies.Count; i++)
        //{
        //    Debug.Log(Vector3.SqrMagnitude(transform.position -+- sortedEnemies[i].transform.position) + " i=" + i);
        //}

        if (sortedEnemies.Count > 0)
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
}

