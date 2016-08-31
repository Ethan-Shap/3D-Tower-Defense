using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player instance;

    public int coins = 100;
    public int health = 100;

	// Use this for initialization
	void Start ()
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void AddHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddCoins(int coins)
    {
        this.coins = coins;
    }

    public int GetCoins()
    {
        return this.coins;
    }

}
