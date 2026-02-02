using UnityEngine;

public class SizeButton : InteractableBase
{
    public enum SizeType
    {
        Grow,
        Shrink
    }

    public enum ScaleMode
    {
        Multiplier,
        DirectScale
    }

    [Header("Button Settings")]
    public SizeType sizeType;
    public ScaleMode scaleMode;

    [Header("Multiplier Mode")]
    public float sizeMultiplier = 1.5f;

    [Header("Direct Scale Mode")]
    public Vector3 targetScale = Vector3.one;

    private bool used = false;

    public override void BeginInteraction()
    {
        if (used) return;

        GameObject player = _playerRB.gameObject;
        if (player == null) return;

        Vector3 newScale = player.transform.localScale;

        if (scaleMode == ScaleMode.Multiplier)
        {
            if (sizeType == SizeType.Grow)
                newScale *= sizeMultiplier;
            else
                newScale /= sizeMultiplier;
        }
        else // DirectScale
        {
            newScale = targetScale;
        }

        player.transform.localScale = newScale;
        used = true;
    }

    public override void EndInteraction()
    {
    }
}
