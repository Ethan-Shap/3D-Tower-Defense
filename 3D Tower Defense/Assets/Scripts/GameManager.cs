using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject selectedTower = null;
    public static GameManager instance;

    [SerializeField]
    public int numRounds = 20;
    [SerializeField]
    public int round = 1;
    public int addNewEnemyAfterRounds = 5;
    public float waitTime = 10f;
    public GameObject startRoundButton;

    private Player p;
    private EnemyManager enemyManager;

    public bool RoundStarted
    {
        get
        {
            return roundStarted;
        }

        set
        {
            startRoundButton.SetActive(!startRoundButton.activeSelf);
            roundStarted = value;
        }
    }
    private bool roundStarted = false;

    private void Awake()
    {
        Debug.Log("Total Rounds " + numRounds);
        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        p = Player.instance;
        enemyManager = EnemyManager.instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void StartRound()
    {
        if (!RoundStarted)
        {
            Debug.Log("Round " + round);
            enemyManager.SpawnEnemies(round);
            RoundStarted = true;
        }
    }

    public void EndRound()
    {
        RoundStarted = false;
        round++;
    }

#if DEBUG
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

        //Speed Up Enemies
        if (GUI.Button(new Rect(500, 50, 50, 50), "5x Speed", myStyle))
        {
            Enemy[] activeEnemies = enemyManager.GetActiveEnemies();
            enemyManager.SpawnRate = 0.01f;
            foreach(Enemy enemy in activeEnemies)
            {
                enemy.Speed = 5;
            }
        }
            

    }
#endif

}
