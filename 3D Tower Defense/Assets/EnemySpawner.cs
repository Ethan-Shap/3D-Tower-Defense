using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [Tooltip("Put enemies in order of difficulty")]
    public GameObject[] enemyPrefabs;
    public float roundEnemySpawnMultiplier = 10f;

    private float spawnRate;
    private float spawnRateMin = 0.1f;
    private float spawnRateMax = 1f;

    private int[] enemySpawnPattern;
    private int maxNumberOfEnemies;

    private List<Transform>[] enemyPools;
    private Transform[] poolParents;
    private GameManager gameManager;
    private PathManager pathManager;

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

        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject newParent = new GameObject("PoolPos" + i);
            poolParents[i] = newParent.transform;
        }

        CalculateAndInstantiateMaxEnemiesInGame(); 
    }

    public void SpawnEnemies(int round)
    {
        int numEnemies = (int)(round * roundEnemySpawnMultiplier);
        int numEnemyTypesToUse = round / gameManager.addNewEnemyAfterRounds;

        StartCoroutine(MoveEnemies(numEnemies, numEnemyTypesToUse));
    }

    private IEnumerator MoveEnemies(int numEnemies, int numEnemyTypesToUse)
    {
        for(int i = 0; i < numEnemies; i++)
        {
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
            maxEnemiesInGame += 2;
            for(int j = 0; j <= maxEnemiesInGame; j++)
            {
                GameObject newEnemy = Instantiate(enemyPrefabs[i], poolPos, Quaternion.identity) as GameObject;
                newEnemy.transform.SetParent(poolParents[i]);
            }
        }
    }

}
