using UnityEngine;
using System.Linq;
using System;

public class SnapToGrid : MonoBehaviour {

    public GameObject snapPosParent;
    public float yOffset, xOffset, zOffset;

    private Transform[] snapPositions;

    private GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
        Transform[] snapPosWithParent = snapPosParent.GetComponentsInChildren<Transform>();
        snapPositions = new Transform[snapPosWithParent.Length - 1];
        for(int i = 0; i < snapPosWithParent.Length; i++)
        {
            if(i > 0)
            {
                snapPositions[i - 1] = snapPosWithParent[i];
            }
        }
        gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.selectedTower)
        {
            float[] distances = new float[snapPositions.Length];
            for(int i = 0; i < snapPositions.Length; i++)
            {
                Vector3 snapPosWithOffset = new Vector3(snapPositions[i].position.x + xOffset, snapPositions[i].position.y, snapPositions[i].position.z + zOffset);
                distances[i] = Vector3.SqrMagnitude(gameManager.selectedTower.transform.position - snapPosWithOffset);
            }
            int minIndex = Array.IndexOf(distances, distances.Min());
            gameManager.selectedTower.transform.position = new Vector3(snapPositions[minIndex].transform.position.x + xOffset, snapPositions[minIndex].transform.position.y + yOffset, snapPositions[minIndex].transform.position.z + zOffset);
            gameManager.selectedTower.transform.SetParent(snapPositions[minIndex].transform);
        }   
	}
}
