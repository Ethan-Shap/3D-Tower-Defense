using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public bool showLevel;

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

    private Player player;
    private EnemyManager enemyManager;
    private MenuManager menuManager;
    private TowerManager towerManager;
    private SaveAndLoad saveAndLoad;
    private SceneManagement sceneManagment;
    private SaveData saveData;

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
        DontDestroyOnLoad(this.gameObject);
        saveData = new SaveData();
    }

    // Use this for initialization
   private void Start()
    {
        player = Player.instance;
        enemyManager = EnemyManager.instance;
        menuManager = GetComponent<MenuManager>();
        towerManager = TowerManager.instance;
        sceneManagment = FindObjectOfType<SceneManagement>();
        saveAndLoad = GetComponent<SaveAndLoad>();
    }

    public void Save()
    {
        PlayerData playerData = player.data;
        LevelData levelData = null;
        if (sceneManagment.CurrentLevel() > 0)
        {
            Enemy[] enemies = enemyManager.GetActiveEnemies();
            Tower[] towers = towerManager.GetActiveTowers().ToArray();
            Dictionary<string, int> towerPositions = new Dictionary<string, int>();
            Dictionary<string, int> enemyPositions = new Dictionary<string, int>();

            foreach (Enemy enemy in enemies)
            {
                enemyPositions.Add(enemy.transform.position.ToString(), (int)enemy.type);
            }

            foreach (Tower tower in towers)
            {
                towerPositions.Add(tower.transform.position.ToString(), (int)tower.type);
            }

            levelData = new LevelData(sceneManagment.CurrentLevel(), towerPositions, enemyPositions, Round, enemyManager.SpawnRate.ToString(), player.Coins, player.Health);
        }
        saveData.playerData = playerData;
        saveData.levelData = levelData;

        saveAndLoad.SaveGameData(saveData);
    }

    public void LoadSaveData()
    {
        saveData = saveAndLoad.LoadGameData();
        player.data = saveData.playerData;
    }

    public void PauseRound()
    {
        GPGManager.instance.UpdateAllAchievements();
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
        player.Coins += startingCoins * coinsMultiplier;
        Round++; 
    }

#if DEBUG
    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle(GUI.skin.button);
        myStyle.fontSize = 8;

        // Add coins to player 
        if (GUI.Button(new Rect(480, 0, 50, 20), "Add Coins", myStyle)) 
            player.Coins += 1000;

        // Add Health to Base
        if (GUI.Button(new Rect(480, 25, 50, 20), "Add Health", myStyle))
            player.Health += 100;

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

public static class ExtensionMethods
{
    public static Vector3 Clamp(this Vector3 v, float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        return new Vector3(Mathf.Clamp(v.x, minX, maxX), Mathf.Clamp(v.y, minY, maxY), Mathf.Clamp(v.z, minZ, maxZ));
    }

    public static Vector3 ClampMagnitude(this Vector3 v, Vector3 point, float minDist, float maxDist)
    {
        float dist = Vector3.Magnitude(v - point);
        Vector3 dir = v - point;
        dir = dir.normalized;
        if (dist <= minDist)
        {
            return (dir * minDist) + point;
        }
        else if (dist >= maxDist)
        {
            return (dir * maxDist) + point;
        }
        else
        {
            return v;
        }
    }

    public static float[] ToIntArray(this Vector3 rVec)
    {
        return new float[] { rVec.x, rVec.y, rVec.z };
    }

}
