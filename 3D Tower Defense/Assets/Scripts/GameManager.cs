using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
    private int numRounds = 20;
    public int NumRounds
    {
        get
        {
            return numRounds;
        }

        set
        {
            numRounds = value;
        }
    }
    [SerializeField]
    private int round = 1;
    public int Round
    {
        get
        {
            return round;
        }

        set
        {
            round = value;
        }
    }

    public int startingCoins = 100;
    public int coinsMultiplier = 2; 

    public int addNewEnemyAfterRounds = 5;
    public float waitTime = 10f;
    public GameObject startRoundButton;
    public GameObject pauseGameButton;

    private Player p;
    private EnemyManager enemyManager;
    private MenuManager menuManager;
    private TowerManager towerManager;
    private bool roundStarted = false;
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

    private void Awake()
    {
        Debug.Log("Total Rounds " + NumRounds);
        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        p = Player.instance;
        enemyManager = EnemyManager.instance;
        menuManager = GetComponent<MenuManager>();
        towerManager = TowerManager.instance;
	}

    public void PauseRound()
    {
        enemyManager.Pause();
        menuManager.OpenScreen(0);
        towerManager.PauseTowers();
    }

    public void ResumeRound()
    {
        enemyManager.Unpause();
        menuManager.CloseScreen(0);
        towerManager.UnpauseTowers();
    }

    public void StartRound()
    {
        if (!RoundStarted)
        {
            Debug.Log("Round " + Round);
            enemyManager.SpawnEnemies(Round);
            RoundStarted = true;
        }
    }

    public void EndRound()
    {
        RoundStarted = false;
        p.Coins += startingCoins * coinsMultiplier;
        Round++;
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
