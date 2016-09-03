using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private Previewer previewer;

	// Use this for initialization
	private void Awake ()
    {
        instance = this;
	}
	
    private void Start()
    {
        previewer = Previewer.instance;
    }

    public GameObject BuildTower(GameObject obj)
    {
        GameObject newTower = (GameObject)Instantiate(obj, Vector3.zero, Quaternion.identity);
        return newTower;
    }

}
