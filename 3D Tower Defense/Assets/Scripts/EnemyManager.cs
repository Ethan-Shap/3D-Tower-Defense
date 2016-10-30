using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;

    public enum EnemyType
    {
        White, Red, Blue, Grey, BOSS
    }

    [Tooltip("Put enemies in order of difficulty")]
    public GameObject[] enemyPrefabs;
    public float enemyMultiplier = 20f;
    public Transform spawnPos;
    public bool showActiveEnemies = false;

    private float spawnRate;
    private float spawnRateMin = 0.7f;
    private float spawnRateMax = 1f;
    private bool canSpawn = true;

    private int[] enemySpawnPattern;
    private int maxNumberOfEnemies;

    private List<Transform>[] enemies;
    private Transform[] poolParents;
    private Transform activeEnemyParent;
    private GameManager gameManager;
    private Transform ALL_ENEMY_PARENT;

    private Path[] paths;

    public float SpawnRate
    {
        get
        {
            return spawnRate;
        }

        set
        {
            spawnRate = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        SpawnRate = Random.Range(spawnRateMin, spawnRateMax);
        ALL_ENEMY_PARENT = new GameObject("ENEMIES").transform;

        if (gameManager.NumRounds % enemyPrefabs.Length != 0)
        {
            gameManager.NumRounds += enemyPrefabs.Length - (gameManager.NumRounds % enemyPrefabs.Length);
        }

        gameManager.addNewEnemyAfterRounds = (gameManager.NumRounds / enemyPrefabs.Length);

        poolParents = new Transform[enemyPrefabs.Length];
        enemies = new List<Transform>[enemyPrefabs.Length];
        paths = GameObject.FindObjectsOfType<Path>();
        activeEnemyParent = new GameObject("Active Enemy").transform;

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemies[i] = new List<Transform>();
        }

        CalculateAndInstantiateMaxEnemiesInGame();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (showActiveEnemies)
        {
            showActiveEnemies = false;
            StartCoroutine(ShowActiveEnemiesInHierarchy());
        }
    }

    private IEnumerator ShowActiveEnemiesInHierarchy()
    {
        Enemy[] enemies = GetActiveEnemies();
        foreach (Enemy enemy in enemies)
        {
            Debug.Log("Hello");
            EditorGUIUtility.PingObject(enemy);
            yield return new WaitForSeconds(1);
        }
    }
#endif

    public void LoadLevel()
    {

    }

    public void SpawnEnemies(int round)
    {
        int numEnemies = (int)(round * enemyMultiplier);
        int numEnemyTypesToUse = round / gameManager.addNewEnemyAfterRounds + 1;
        Debug.Log("Number of enemies to use " + numEnemyTypesToUse);
        Debug.Log("Number of Enemies " + numEnemies);
        Debug.Log(enemies[0].Count);

        StartCoroutine(MoveEnemies(round, numEnemies, numEnemyTypesToUse));
    }

    private IEnumerator MoveEnemies(int round, int numEnemies, int numTypesToUse)
    {
        int typeToSpawn = 0;
        float timeStarted = Time.time;

        for(int i = 0; i < numEnemies; i++)
        {
            if (numTypesToUse > 1 && i % gameManager.addNewEnemyAfterRounds == 0)
            {
                typeToSpawn = RandomNewEnemyType(typeToSpawn, numTypesToUse);
                Debug.Log("Type to spawn " + typeToSpawn);
            }
            //Debug.Log(i);

            if (enemies[typeToSpawn].Count == 0)
                CreateExtraEnemies();

            enemies[typeToSpawn][0].gameObject.SetActive(true);
            enemies[typeToSpawn][0].transform.position = spawnPos.position;
            enemies[typeToSpawn][0].GetComponent<Enemy>().currentPath = paths[paths.Length >= 2 ? i % paths.Length : 0];
            enemies[typeToSpawn][0].SetParent(activeEnemyParent.transform);
            enemies[typeToSpawn].RemoveAt(0);

            yield return new WaitForSeconds(SpawnRate);
            while (!canSpawn)
            {
                yield return null;
            }
        }
        while (activeEnemyParent.transform.childCount > 0)
        {
            yield return null;
        }

        //Debug.Log(Time.time - timeStarted);

        gameManager.EndRound();
   }

    public void Pause()
    {
        canSpawn = false;

        PauseEnemies();
    }

    private void PauseEnemies()
    {
        Enemy[] activeEnemies = GetActiveEnemies();
        foreach (Enemy enemy in activeEnemies)
        {
            enemy.Speed = 0;
        }
    }

    public void Unpause()
    {
        canSpawn = true;
        UnpauseEnemies();
    }

    private void UnpauseEnemies()
    {
        Enemy[] activeEnemies = GetActiveEnemies();
        foreach (Enemy enemy in activeEnemies)
        {
            enemy.Speed = enemy.DefaultSpeed;
        }
    }

    private void CreateExtraEnemies()
    {

    }

    public void ResetPosition(Enemy enemy)
    {
        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (enemyPrefabs[i].GetComponent<Enemy>().type == enemy.type)
            {
                enemy.transform.SetParent(poolParents[i].transform);
                enemy.gameObject.SetActive(false);
                enemy.transform.position = poolParents[i].transform.position;
                enemies[i].Add(enemy.transform);
            }
        }
    }

    public Enemy[] GetActiveEnemies()
    {
        return activeEnemyParent.GetComponentsInChildren<Enemy>();
    }

    private int RandomNewEnemyType(int currentType, int numTypes)
    {
        int newType = currentType;
        while (newType == currentType)
            newType = Random.Range(0, numTypes);

        return newType;
    }

    private void CalculateAndInstantiateMaxEnemiesInGame()
    {
        Vector3 poolPos = new Vector3(0, -100, 0);

        float totalPathDist = 0;

        if (paths.Length == 0)
            throw new UnityException("No Paths to calculate distance from");

        float longestDist = 0;
        for (int i = 0; i < paths.Length; i++)
        {
            if(longestDist < paths[i].TotalDistance())
            {
                longestDist = paths[i].TotalDistance();
            }
        }
        totalPathDist = longestDist;

        Debug.Log("Spawn Rate " + SpawnRate);
        Debug.Log("Total Distance " + totalPathDist);

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            poolPos += new Vector3(0, -100, 0);

            GameObject newParent = new GameObject(enemyPrefabs[i].GetComponent<Enemy>().type.ToString() + i);
            newParent.transform.position = poolPos;
            newParent.transform.SetParent(ALL_ENEMY_PARENT);
            poolParents[i] = newParent.transform;

            float travelTime = (totalPathDist / (enemyPrefabs[i].GetComponent<Enemy>().Speed));
            Debug.Log("Travel time " + travelTime);
            Debug.Log("max enemies in game " + (travelTime / SpawnRate));
            int maxEnemiesInGame = Mathf.RoundToInt(travelTime / spawnRate);

            // Add 5 Extra enemies just in case 
            maxEnemiesInGame += 5;

            for(int j = 0; j <= maxEnemiesInGame; j++)
            {
                GameObject newEnemy = Instantiate(enemyPrefabs[i], poolPos, Quaternion.identity) as GameObject;
                newEnemy.transform.SetParent(poolParents[i]);
                newEnemy.SetActive(false);
                enemies[i].Add(newEnemy.transform);
            }
        }
    }
}
