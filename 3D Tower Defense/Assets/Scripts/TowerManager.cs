using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {

    public static TowerManager instance;
    public Tower selectedTower;

    public enum TowerType { ice_tower, fireball_tower, arrow_tower, fire_tower }

    private Transform mainTowerParent;
    private Transform[] towerTypesParent;
    private BuildManager buildManager;
    private Previewer previewer;
    private SnapToGrid snapToGrid;
    private List<Tower> towers;

    private void Awake()
    {
        instance = this;
        towers = new List<Tower>();
        towers.AddRange(FindObjectsOfType<Tower>());
    }

	// Use this for initialization
	private void Start ()
    {
        buildManager = BuildManager.instance;
        snapToGrid = SnapToGrid.instance;
        previewer = Previewer.instance;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (selectedTower)
        {
            if (!selectedTower.placed)
            {
                snapToGrid.SnapSelectedTowerToGrid();
                previewer.PreviewTower(selectedTower.gameObject);
            }
        }
	}

    public Tower BuildTower(GameObject tower)
    {
        TouchInput.currentControl = TouchInput.ControlType.Previewing;
        GameObject newTower = buildManager.BuildTower(tower);
        selectedTower = newTower.GetComponent<Tower>();

        return newTower.GetComponent<Tower>();
    }

    public void DestroyTower(GameObject tower)
    {
        Destroy(tower);
    }

    public void PauseTowers()
    {
        foreach(Tower tower in towers)
        {
            if (tower.GetComponentInChildren<ParticleSystem>())
            {
                tower.GetComponentInChildren<ParticleSystem>().Pause();
            }
        }
    }

    public void UnpauseTowers()
    {
        foreach (Tower tower in towers)
        {
            if (tower.GetComponentInChildren<ParticleSystem>())
            {
                tower.GetComponentInChildren<ParticleSystem>().Pause();
            }
        }
    }

    public void PlaceTower(GameObject tower)
    {
        TouchInput.currentControl = TouchInput.ControlType.Regular;
        selectedTower.placed = true;
        previewer.ExitPreview(tower);
    }

}
