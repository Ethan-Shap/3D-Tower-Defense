using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner instance;

    [Tooltip("Put enemies in order of difficulty")]
    public GameObject[] enemyPrefabs;
    public float enemyMultiplier = 20f;

    private float spawnRate;
    private float spawnRateMin = 0.1f;
    private float spawnRateMax = 1f;

    private int[] enemySpawnPattern;
    private int maxNumberOfEnemies;

    private List<Transform> enemies;
    private Transform[] poolParents;
    private GameManager gameManager;
    private PathManager pathManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        pathManager = PathManager.instance;

        if (gameManager.numRounds % enemyPrefabs.Length != 0)
        {
            gameManager.numRounds += enemyPrefabs.Length - (gameManager.numRounds % enemyPrefabs.Length);
        }

        gameManager.addNewEnemyAfterRounds = (gameManager.numRounds / enemyPrefabs.Length);
        poolParents = new Transform[enemyPrefabs.Length];
        enemies = new List<Transform>();

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject newParent = new GameObject("PoolPos" + i);
            poolParents[i] = newParent.transform;
        }

        CalculateAndInstantiateMaxEnemiesInGame();
    }

    public void SpawnEnemies(int round)
    {
        int numEnemies = (int)(round * enemyMultiplier);
        int numEnemyTypesToUse = round / gameManager.addNewEnemyAfterRounds + 1;

        StartCoroutine(MoveEnemies(round, numEnemies, numEnemyTypesToUse));
    }

    private IEnumerator MoveEnemies(int round, int numEnemies, int numEnemyTypesToUse)
    {
        int enemyTypeToSpawn = 0;
        int[] numEnemiesUsed = new int[numEnemyTypesToUse];
        for(int i = 0; i < numEnemies; i++)
        {
            if (numEnemyTypesToUse > 1)
            {
                int randomType = Random.Range(0, numEnemyTypesToUse);
                while(randomType == enemyTypeToSpawn)
                    randomType = Random.Range(0, numEnemyTypesToUse);

                enemyTypeToSpawn = randomType;
            }

            Debug.Log(numEnemiesUsed[enemyTypeToSpawn]);
            enemies[numEnemiesUsed[enemyTypeToSpawn]].transform.position = transform.position;
            numEnemiesUsed[enemyTypeToSpawn]++;
            yield return new WaitForSeconds(spawnRate);
        }
    }


    private int RandomizeSpawn(int numEnemies)
    {
        return Random.Range(0, numEnemies + 1);
    }

    private void CalculateAndInstantiateMaxEnemiesInGame()
    {
        Vector3 poolPos = new Vector3(0, -100, 0);
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            poolPos += new Vector3(0, -100, 0);
            float travelTime = (pathManager.GetPathDistance() / enemyPrefabs[i].GetComponent<Enemy>().Speed);
            int maxEnemiesInGame = Mathf.RoundToInt(spawnRate * travelTime);

            // Add 2 Extra enemies just in case 
            maxEnemiesInGame += 10;

            for(int j = 0; j <= maxEnemiesInGame; j++)
            {
                GameObject newEnemy = Instantiate(enemyPrefabs[i], poolPos, Quaternion.identity) as GameObject;
                newEnemy.transform.SetParent(poolParents[i]);
                enemies.Add(newEnemy.transform);
            }
        }
    }

}
