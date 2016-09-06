using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [Tooltip("Put enemies in order of difficulty")]
    public GameObject[] enemyPrefabs;

    private List<Transform>[] enemyPools;
    private GameManager gameManager;

    private void Start()
    {
        if (gameManager.numRounds % enemyPrefabs.Length != 0)
        {
            gameManager.numRounds += enemyPrefabs.Length - (gameManager.numRounds % enemyPrefabs.Length);
        }

        gameManager.addNewEnemyAfterRounds = (gameManager.numRounds / enemyPrefabs.Length); 
    }

    public void SpawnEnemies(int numEnemies, int numEnemyTypesToSpawn)
    {
        int numEnemiesSpawned = 0;

        for (int i = 0; i < numEnemies; i++)
        { 


            enemyPools[enemyToSpawn][i].transform.position = transform.position;
            enemiesUsed[enemyToSpawn]++;
        }
    }

    private int RandomizeSpawn(int numEnemies)
    {
        return Random.Range(0, numEnemies + 1);
    }

    private void InstantiateEnemyPools()
    {

    }

    private void CalculateMaxNumberOfEnemiesInGame()
    {

    }

}
