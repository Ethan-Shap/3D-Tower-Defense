using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public Transform[] screens;

    private void Awake()
    { 
        
    }
		
	private void Start()
	{
	
	}

	private void Update()
	{
	
	}

    public void OpenScreen(int screenIndex)
    {
        if (screenIndex >= 0 && screenIndex < screens.Length)
        {
            screens[screenIndex].gameObject.SetActive(true);

            for (int i = 0; i < screens.Length; i++)
            {
                if (i != screenIndex)
                    screens[i].gameObject.SetActive(false);
            }
        }
    }

    public void CloseScreen(int screenIndex)
    {
        if (screenIndex >= 0 && screenIndex < screens.Length)
        {
            screens[screenIndex].gameObject.SetActive(false);
        }
    }
}