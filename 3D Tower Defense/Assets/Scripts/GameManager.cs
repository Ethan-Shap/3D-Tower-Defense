using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject selectedTower = null;
    public static GameManager instance;

    [SerializeField]
    public int numRounds = 20;
    public int addNewEnemyAfterRounds = 5;
    public bool gameStarted = false;
    public float waitTime = 10f;

    private Player p;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        Debug.Log("Total Rounds " + numRounds);
        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        p = Player.instance;
        enemySpawner = EnemySpawner.instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.realtimeSinceStartup > waitTime && gameStarted == false)
        {
            enemySpawner.SpawnEnemies(8);
            gameStarted = true;
        }
	}

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle(GUI.skin.button);
        myStyle.fontSize = 8;

        // Add coins to player 
        if (GUI.Button(new Rect(480, 0, 50, 20), "Add Coins", myStyle)) 
            p.Coins += 1000;

        // Add Health to Base
        if (GUI.Button(new Rect(480, 25, 50, 20), "Add Health", myStyle))
            p.Health += 100;

    }

}
