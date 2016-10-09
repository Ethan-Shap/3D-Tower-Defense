using UnityEngine;
using System.Linq;
using System;

public class SnapToGrid : MonoBehaviour {

    public static SnapToGrid instance;

    public GameObject snapPosParent;
    public float yOffset, xOffset, zOffset;

    private Transform[] snapPositions;

    private TowerManager towerManager;
    private Transform towerParent;

    private void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        towerParent = new GameObject("PlacedTowers").transform;
        towerParent.transform.SetParent(transform);

        Transform[] snapPosWithParent = snapPosParent.GetComponentsInChildren<Transform>();
        snapPositions = new Transform[snapPosWithParent.Length - 1];
        for(int i = 0; i < snapPosWithParent.Length; i++)
        {
            if(i > 0)
            {
                snapPositions[i - 1] = snapPosWithParent[i];
            }
        }
        towerManager = TowerManager.instance;
	}

    public void SnapSelectedTowerToGrid()
    {
        Tower currentTower = towerManager.selectedTower;
        if (currentTower)
        {
            float[] distances = new float[snapPositions.Length];
            for (int i = 0; i < snapPositions.Length; i++)
            {
                Vector3 snapPosWithOffset = new Vector3(snapPositions[i].position.x + xOffset, snapPositions[i].position.y + yOffset, snapPositions[i].position.z + zOffset);
                distances[i] = Vector3.SqrMagnitude(currentTower.transform.position - snapPosWithOffset);
            }

            int minIndex = Array.IndexOf(distances, distances.Min());
            currentTower.transform.position = new Vector3(snapPositions[minIndex].transform.position.x + xOffset, snapPositions[minIndex].transform.position.y + yOffset, snapPositions[minIndex].transform.position.z + zOffset);
            Debug.Log(snapPositions[minIndex].transform.position.y + yOffset);
            currentTower.transform.SetParent(towerParent);
        }
    }
}
