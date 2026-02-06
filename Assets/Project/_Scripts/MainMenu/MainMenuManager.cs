using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Name")]
    public string gameSceneName = "GameScene";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        Debug.Log("Options açýlýyor...");
    }

    public void OpenExtras()
    {
        Debug.Log("Extras açýlýyor...");
    }

    public void QuitGame()
    {
        Debug.Log("Oyun kapatýlýyor...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}