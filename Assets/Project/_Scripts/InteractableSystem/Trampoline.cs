using DG.Tweening;
using UnityEngine;

public class Trampoline : InteractableBase
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 20f;

    [Header("Visual Bounce")]
    [SerializeField] private float downAmount = 0.3f;
    [SerializeField] private float bounceDuration = 0.1f;

    private Vector3 startPos;
    private Tween bounceTween;

    private void Start()
    {
        startPos = transform.position;
    }

    public override void BeginInteraction()
    {
        if (_playerRB == null) return;

        // Y hýzýný sýfýrla
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
        if (bounceTween != null && bounceTween.IsActive())
            bounceTween.Kill();

        bounceTween = transform
            .DOMoveY(startPos.y - downAmount, bounceDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform
                    .DOMoveY(startPos.y, bounceDuration)
                    .SetEase(Ease.OutBounce);
            });
    }
}
