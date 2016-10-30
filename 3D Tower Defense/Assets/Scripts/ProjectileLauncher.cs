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
    private int damage;
    private Transform projectilesParent;
    private bool canShoot = true;

	private void Awake()
	{
        nextShootTime = fireRate;
        
        if (!projectilePrefab)
            throw new System.NullReferenceException("No projectile for " + name + " to shoot");

        tower = GetComponent<Tower>();
        damage = tower.damage;
        projectilesParent = new GameObject("Projectiles").transform;
        projectilesParent.SetParent(this.transform);
	}

    private void Update()
    {
        if (canShoot)
        {
            Debug.Log("shooting");
            if (Time.time >= nextShootTime && tower.GetCurrentEnemy())
            {
                nextShootTime = Time.time + fireRate;
                ShootProjectile();
            }

            if (rotateLaunchPos && tower.GetCurrentEnemy())
                PointAtEnemy();
        }
    }

    public void Pause()
    {
        canShoot = false;
        Projectile[] projectiles = projectilesParent.GetComponentsInChildren<Projectile>();

        foreach(Projectile projectile in projectiles)
        {
            if (projectile.GetComponent<ParticleSystem>())
            {
                projectile.GetComponent<ParticleSystem>().Pause();
            }
            projectile.CanMove = false;
        }
    }

    public void Unpause()
    {
        canShoot = true;
        Projectile[] projectiles = projectilesParent.GetComponentsInChildren<Projectile>();
        foreach (Projectile projectile in projectiles)
        {
            if (projectile.GetComponent<ParticleSystem>())
            {
                projectile.GetComponent<ParticleSystem>().Play();
            }
            projectile.CanMove = true;
        }
    }

    private void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, launchPos.position, Quaternion.identity) as GameObject;
        Enemy currentEnemy = tower.GetCurrentEnemy();
        newProjectile.AddComponent<Projectile>().SetProjectileVars(currentEnemy.transform, projectileSpeed, damage, this);
        newProjectile.transform.SetParent(projectilesParent);
    }

    private void PointAtEnemy()
    {
        Vector3 dir = tower.GetCurrentEnemy().transform.position - launchPos.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        launchPos.transform.rotation = Quaternion.Lerp(launchPos.transform.rotation, lookRot, 5f * Time.deltaTime);
    }

    public Transform ClosestEnemy()
    {
        if (tower.GetCurrentEnemy())
            return tower.GetCurrentEnemy().transform;
        else if (tower.GetClosestEnemy())
            return tower.GetClosestEnemy().transform;
        else
            return null;
    }
}