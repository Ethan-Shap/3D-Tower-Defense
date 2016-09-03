using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player instance;

    private int coins = 100;
    private int health = 100;
    private Display display;

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

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        display = Display.instance;
	}

    public void OpenTowerUpgradeWindow()
    {

    }

}
