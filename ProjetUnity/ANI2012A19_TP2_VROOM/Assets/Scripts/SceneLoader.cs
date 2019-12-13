using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainScene()
    {
        this.LoadScene("MainScene");
    }

    public void GoToFullScreenScreen()
    {
        this.LoadScene("FullScreen");
    }
}
