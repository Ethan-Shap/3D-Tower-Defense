using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataHelper : MonoBehaviour {

    public static DataHelper instance;

    public Canvas currentCanvas;
    public GameObject dataNotification;

    public float widthPercent, heightPercent, xPositionsPercent;

	private void Awake()
	{
        instance = this;
	}
		
	private void Start()
	{
	
	}

	private void Update()
	{
	
	}

    public void ShowData(string data)
    {
        GameObject newDataNotificaiton = Instantiate(dataNotification, currentCanvas.transform) as GameObject;
        newDataNotificaiton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * widthPercent, Screen.height * heightPercent);
        newDataNotificaiton.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.5f * xPositionsPercent, currentCanvas.transform.position.y, currentCanvas.transform.position.z);
        newDataNotificaiton.GetComponent<RectTransform>().localScale = Vector3.one;
        newDataNotificaiton.GetComponentInChildren<Text>().text = data;
    }

    public void ShowDataWithResponse(string data)
    {

    }

}