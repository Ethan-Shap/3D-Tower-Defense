using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {

    public enum TowerType { ice_tower, fireball_tower, arrow_tower, fire_tower }

    private Transform mainTowerParent;
    private Transform[] towerTypesParent;
    private BuildManager buildManager;

	// Use this for initialization
	private void Start ()
    {
        buildManager = BuildManager.instance;
	}
	
	// Update is called once per frame
	private void Update ()
    {
	    
	}

    public Tower BuildTower()
    {
        buildManager.BuildTower();
    }

}
