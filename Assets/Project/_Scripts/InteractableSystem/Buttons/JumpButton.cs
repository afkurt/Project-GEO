using UnityEngine;

public class JumpButton : InteractableBase
{
    [Header("Jump Settings")]
    [Tooltip("1.5 = %50 arttýrýr, 0.7 = %30 azaltýr")]
    public float jumpMultiplier = 1.2f;

    private bool used;

    public override void BeginInteraction()
    {
        if (used) return;

        GameObject player = _playerRB.gameObject;
        if (!player) return;

        PlayerController movement = player.GetComponent<PlayerController>();
        if (movement == null) return;

        movement.jumpForce *= jumpMultiplier;

        used = true;
    }

    public override void EndInteraction()
    {
    }
}
