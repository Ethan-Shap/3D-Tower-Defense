using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text.RegularExpressions;

public class SceneManagement : MonoBehaviour {	

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

	public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int CurrentLevel()
    {
        int level = -1;
        int.TryParse(Regex.Replace(SceneManager.GetActiveScene().name, @"\D", string.Empty), out level);
        return level;
    }
}