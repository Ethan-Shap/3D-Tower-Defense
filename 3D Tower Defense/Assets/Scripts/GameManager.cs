using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int numRounds = 20;

    [SerializeField]
    private int numRdsBetweenEnemyAdd = 5;

    private Player p;

	// Use this for initialization
	void Start ()
    {
        p = Player.instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle(GUI.skin.button);
        myStyle.fontSize = 8;

        // Add coins to player 
        if (GUI.Button(new Rect(480, 0, 50, 20), "Add Coins", myStyle)) 
            p.AddCoins(1000);

        // Add Health to Base
        if (GUI.Button(new Rect(480, 25, 50, 20), "Add Health", myStyle))
            p.AddHealth(100);

    }

}
