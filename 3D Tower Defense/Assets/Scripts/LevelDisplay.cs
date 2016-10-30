using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelDisplay : MonoBehaviour {

    public static LevelDisplay instance;

    public Text healthText;
    public Text coinsText;

    private Player player;

    private void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        player = Player.instance;

        coinsText.text = player.Coins.ToString();
        healthText.text = player.Health.ToString();
	}

    public void SetCoinsText(string text)
    {
        coinsText.text = text;
    }

    public string GetCoinsText()
    {
        return coinsText.text;
    }

    public void SetHealthText(string text)
    {
        healthText.text = text;
    }

    public string GetHealthText()
    {
        return healthText.text;
    }

}
