﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    [SerializeField]
    private int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                EnemyManager.instance.ResetPosition(this.gameObject.GetComponent<Enemy>());
            }
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddHealth(int health)
    {
        this.health += health;
    }

}