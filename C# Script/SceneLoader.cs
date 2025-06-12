using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadDatabaseScene()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitApp()
    {
        Debug.Log("Exit App");  // For testing inside Unity Editor
        Application.Quit();     // Actually quits app on a real device or build
    }
}
