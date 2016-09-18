using UnityEngine;
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

    private float spawnRate;
    private float spawnRateMin = 0.7f;
    private float spawnRateMax = 1f;

    private int[] enemySpawnPattern;
    private int maxNumberOfEnemies;

    private List<Transform>[] enemies;
    private Transform[] poolParents;
    private Transform activeEnemyParent;
    private GameManager gameManager;

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

        if (gameManager.numRounds % enemyPrefabs.Length != 0)
        {
            gameManager.numRounds += enemyPrefabs.Length - (gameManager.numRounds % enemyPrefabs.Length);
        }

        gameManager.addNewEnemyAfterRounds = (gameManager.numRounds / enemyPrefabs.Length);

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

    public void SpawnEnemies(int round)
    {
        int numEnemies = (int)(round * enemyMultiplier);
        int numEnemyTypesToUse = round / gameManager.addNewEnemyAfterRounds + 1;
        Debug.Log("Number of enemies to use " + numEnemyTypesToUse);
        Debug.Log("Number of Enemies " + numEnemies);

        StartCoroutine(MoveEnemies(round, numEnemies, numEnemyTypesToUse));
    }

    private IEnumerator MoveEnemies(int round, int numEnemies, int numTypesToUse)
    {
        int typeToSpawn = 0;

        for(int i = 0; i < numEnemies; i++)
        {
            if (numTypesToUse > 1 && i % gameManager.addNewEnemyAfterRounds == 0)
            {
                typeToSpawn = RandomNewEnemyType(typeToSpawn, numTypesToUse);
                Debug.Log("Type to spawn " + typeToSpawn);
            }

            //Debug.Log(typeToSpawn);
            //Debug.Log(numUsed[typeToSpawn]);
            //Debug.Log(enemies.Count);
            //Debug.Log(enemies[numUsed[typeToSpawn]]);

            //Debug.Log(i);

            if (enemies[typeToSpawn].Count == 0)
                CreateExtraEnemies();

            enemies[typeToSpawn][0].transform.position = transform.position;
            enemies[typeToSpawn][0].GetComponent<Enemy>().currentPath = paths[paths.Length >= 2 ? i % paths.Length : 0];
            enemies[typeToSpawn][0].gameObject.SetActive(true);
            enemies[typeToSpawn][0].SetParent(activeEnemyParent.transform);
            enemies[typeToSpawn].RemoveAt(0);

            yield return new WaitForSeconds(SpawnRate);
        }
        while (activeEnemyParent.transform.childCount > 0)
        { 
            yield return new WaitForSeconds(0.25f);
        }
        
        gameManager.EndRound();
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

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            poolPos += new Vector3(0, -100, 0);

            GameObject newParent = new GameObject("PoolPos" + i);
            newParent.transform.position = poolPos;
            poolParents[i] = newParent.transform;

            float travelTime = (totalPathDist / enemyPrefabs[i].GetComponent<Enemy>().Speed + totalPathDist % enemyPrefabs[i].GetComponent<Enemy>().Speed);
            int maxEnemiesInGame = Mathf.RoundToInt(travelTime / (1 + SpawnRate));

            // Add 10 Extra enemies just in case 
            maxEnemiesInGame += 10;

            for(int j = 0; j <= maxEnemiesInGame; j++)
            {
                GameObject newEnemy = Instantiate(enemyPrefabs[i], poolPos, Quaternion.identity) as GameObject;
                newEnemy.transform.SetParent(poolParents[i]);
                enemies[i].Add(newEnemy.transform);
            }
        }
    }
}
