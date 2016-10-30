using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuDisplay : MonoBehaviour {

    public Text playerNameText;

    /// <summary>
    /// Show Leaderboards
    /// </summary>
	public void ShowLeaderboards()
    {
        GPGManager.instance.OpenLeaderboards();
    }

    /// <summary>
    /// Show google play achievements 
    /// </summary>
    public void ShowAchievements()
    {
        GPGManager.instance.OpenAchievements();
    }

    /// <summary>
    /// Open up the market page for my app
    /// </summary>
    public void RateTheApp()
    {
       
    }

    /// <summary>
    /// Share the app
    /// </summary>
    public void ShareTheApp()
    {

    }

    public void ConnectToGooglePlay()
    {

    }

    public void ShowPersonalStats()
    {

    }

}