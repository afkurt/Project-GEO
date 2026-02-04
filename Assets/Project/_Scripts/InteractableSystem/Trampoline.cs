using DG.Tweening;
using UnityEngine;

public class Trampoline : InteractableBase
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 20f;

    [Header("Visual Bounce")]
    [SerializeField] private float squashAmount = 0.2f;
    [SerializeField] private float squashDuration = 0.1f;

    private Vector3 startScale;
    private Tween squashTween;

    private void Start()
    {
        startScale = transform.localScale;
    }

    public override void BeginInteraction()
    {
        if (_playerRB == null) return;

        // Y ekseni hýzýný sýfýrla 
        _playerRB.linearVelocity = new Vector2(_playerRB.linearVelocity.x, 0f);

        // Yukarý zýplat
        _playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Trambolin animasyonu
        PlayBounce();
    }

    public override void EndInteraction()
    {
    }

    private void PlayBounce()
    {
        if (squashTween != null && squashTween.IsActive())
            squashTween.Kill();

        squashTween = transform
            .DOScaleY(startScale.y - squashAmount, squashDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform
                    .DOScaleY(startScale.y, squashDuration)
                    .SetEase(Ease.OutBounce);
            });
    }
}
