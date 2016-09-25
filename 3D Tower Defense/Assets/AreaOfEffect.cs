using UnityEngine;
using System.Collections;

public class AreaOfEffect : MonoBehaviour {

    public Transform partToRotate;
    public float rotSpeed;

    private Tower tower;
    private ParticleSystem partSystem;

	private void Awake()
	{
        tower = GetComponent<Tower>();
        partSystem = GetComponentInChildren<ParticleSystem>();
	}

    private void Update()
    {
        if (tower.GetCurrentEnemy())
        {
            partSystem.Play();
            PointAtEnemy();
        }
        else
        {
            partSystem.Stop();
        }
    }

    private void PointAtEnemy()
    {
        Vector3 dir = tower.GetCurrentEnemy().transform.position - partToRotate.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        partToRotate.transform.rotation = Quaternion.Lerp(partToRotate.transform.rotation, lookRot, rotSpeed * Time.deltaTime);
    }

}