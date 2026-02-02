using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Target Scene")]
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("LevelExit: Next Scene Name empty!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
