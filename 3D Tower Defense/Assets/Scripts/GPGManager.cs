using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class GPGManager : MonoBehaviour {

    public static GPGManager instance;
    public static bool isConnectedToGPG = false;

	private void Awake()
	{
        instance = this;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        .Build();
        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
        ConnectToGooglePlayGames();
    }

    public void UpdateAchievement(string achievementID, double progress)
    {
        Social.ReportProgress(achievementID, progress, (success) =>
        {

        });
    }

    public void UpdateIncrementalAchievement(string achievementID, int steps)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(achievementID, steps, (success) =>
        {

        });
    }

    public void UpdateAllAchievements()
    {

    }

    public void RateTheApp()
    {
        Application.OpenURL("market://details?id=<com.EthanShapiro.TowerDefense>");
    }

    public void OpenLeaderboards()
    {
        Social.ShowLeaderboardUI();
    }

    public void OpenAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void ConnectToGooglePlayGames()
    {
        Social.localUser.Authenticate((success) =>
        {
            isConnectedToGPG = success;
            if(success == true)
            {
                DataHelper.instance.ShowData("Successfully logged in");
            } else
            {
                DataHelper.instance.ShowData("Failed to log in");
            }
        });
    }

}