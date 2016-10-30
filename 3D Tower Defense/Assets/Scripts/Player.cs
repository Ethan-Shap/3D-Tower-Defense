using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public static Player instance;

    private int coins = 100;
    private int health = 100;
    private LevelDisplay display;
    private string playerName = "BANANA";
    private int levelsUnlocked = 1;
    public PlayerData data = new PlayerData();

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            display.SetHealthText(health.ToString());
            health = value;
        }
    }
    public int Coins
    {
        get
        {
            return coins;
        }

        set
        {
            coins = value;
            display.SetCoinsText(coins.ToString());
        }
    }
    public string Name
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }
    public int LevelsUnlocked
    {
        get
        {
            return levelsUnlocked;
        }

        set
        {
            levelsUnlocked = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        display = LevelDisplay.instance;
	}
}

