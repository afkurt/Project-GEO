using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : InteractableBase
{
    public override void BeginInteraction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void EndInteraction()
    {
    }
}
