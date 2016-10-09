using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player instance;

    private int coins = 100;
    private int health = 100;
    private Display display;
    private string name;
    private int levelsUnlocked = 1;

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
            return name;
        }

        set
        {
            name = value;
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

    // Use this for initialization
    void Start ()
    {
        display = Display.instance;
	}

}
