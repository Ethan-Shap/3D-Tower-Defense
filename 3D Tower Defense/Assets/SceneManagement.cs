using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {	

	public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}