using UnityEngine;
using System.Collections;

public class AreaOfEffect : MonoBehaviour {

    public Transform partToRotate;
    public float rotSpeed;
    public float attackSpeed;
    public float damageAngle;

    private Tower tower;
    private ParticleSystem partSystem;
    private float nextAttackTime = 0;
    private bool canAttack = false;

    public void Pause()
    {
        partSystem.Pause();
    }

    public void Unpause()
    {
        partSystem.Play();
    }

	private void Awake()
	{
        tower = GetComponent<Tower>();
        partSystem = GetComponentInChildren<ParticleSystem>();
	}

    private void Update()
    {
        if (tower.GetClosestEnemy())
            PointAtEnemy();

        if (tower.GetCurrentEnemy() && CanAttack() && !partSystem.isPaused)
        {
            if (nextAttackTime <= Time.time)
            {
                // Deal damage to enemy
                nextAttackTime = Time.time + attackSpeed;
                tower.GetCurrentEnemy().GetComponent<Health>().TakeDamage(tower.damage);
            }
            partSystem.Play();
        }
        else
        {
            partSystem.Stop();
        }
    }

    private bool CanAttack()
    {
        float angle = Vector3.Angle(partToRotate.transform.position, tower.GetCurrentEnemy().transform.position);
        //Debug.Log(angle);

        return (angle <= damageAngle);
    }

    private void PointAtEnemy()
    {
        Vector3 dir = tower.GetClosestEnemy().transform.position - partToRotate.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        partToRotate.transform.rotation = Quaternion.Lerp(partToRotate.transform.rotation, lookRot, rotSpeed * Time.deltaTime);
    }

}