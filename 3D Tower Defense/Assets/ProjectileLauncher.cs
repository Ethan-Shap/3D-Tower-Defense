using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLauncher : MonoBehaviour {

    public GameObject projectilePrefab;

    public float projectileSpeed;
    public float fireRate = 0.25f;

    public Transform launchPos;
    public bool rotateLaunchPos = false;

    private Tower tower;
    private float nextShootTime;
    private Animator launcherAnimator;
    public float animOffset = 1;

	private void Awake()
	{
        nextShootTime = fireRate;

        if (!projectilePrefab)
            throw new System.NullReferenceException("No projectile for " + name + " to shoot");

        tower = GetComponent<Tower>();
        launcherAnimator = launchPos.gameObject.GetComponent<Animator>();
	}

    private void Update()
    {
        if (!launcherAnimator)
        {
            if (Time.time >= nextShootTime && tower.GetCurrentEnemy())
            {
                nextShootTime = Time.time + fireRate;
                ShootProjectile();
            }
        }
        else
        {
            if (Time.time >= nextShootTime && tower.GetCurrentEnemy())
            {
                nextShootTime = Time.time + fireRate;
                launcherAnimator.SetBool("shoot", true);
                ShootProjectile();
            } else if (tower.GetCurrentEnemy())
            {
                PointAtEnemy();
                launcherAnimator.SetBool("shoot", true);
            } else
            {
                launcherAnimator.SetBool("shoot", false);
            }
        }
    }

    private void ShootProjectile()
    {
        Debug.DrawRay(launchPos.position, tower.GetCurrentEnemy().transform.position);
        GameObject newProjectile = Instantiate(projectilePrefab, launchPos.position, Quaternion.identity) as GameObject;
        Enemy currentEnemy = tower.GetCurrentEnemy();
        newProjectile.AddComponent<Projectile>().SetProjectileVars(currentEnemy.transform, projectileSpeed);
    }

    private void PointAtEnemy()
    {
        Vector3 dir = tower.GetCurrentEnemy().transform.position - launchPos.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        launchPos.transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, 1f);
    }

}