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

	private void Awake()
	{
        nextShootTime = fireRate;

        if (!projectilePrefab)
            throw new System.NullReferenceException("No projectile for " + name + " to shoot");

        tower = GetComponent<Tower>();
        damage = tower.damage;
	}

    private void Update()
    {
        if (Time.time >= nextShootTime && tower.GetCurrentEnemy())
        {
            nextShootTime = Time.time + fireRate;
            ShootProjectile();
        }

        if (rotateLaunchPos && tower.GetCurrentEnemy())
            PointAtEnemy();
    }

    private void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, launchPos.position, Quaternion.identity) as GameObject;
        Enemy currentEnemy = tower.GetCurrentEnemy();
        newProjectile.AddComponent<Projectile>().SetProjectileVars(currentEnemy.transform, projectileSpeed, damage, this);
    }

    private void PointAtEnemy()
    {
        Debug.Log("Hello");
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