using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private MoveDirection2D direction;
    [SerializeField] private float distance = 3f;
    [SerializeField] private float duration = 1f;

    [Header("Behaviour")]
    [SerializeField] private bool moveOnStart = true;
    [SerializeField] private bool loop = true;
    [SerializeField] private AnimationCurve moveCurve;



    private Tween moveTween;
    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;

        if (moveOnStart)
            StartMovement();
    }

    public void StartMovement()
    {
        if (moveTween != null && moveTween.IsActive())
            return;

        Vector2 targetPos = startPos + GetDirectionVector() * distance;

        moveTween = transform.DOMove(targetPos, duration)
            .SetEase(moveCurve)
            .SetAutoKill(false)
            .SetLink(gameObject);

        if (loop)
            moveTween.SetLoops(-1, LoopType.Yoyo);
    }

    public void StopMovement()
    {
        moveTween?.Kill();
    }

    private Vector2 GetDirectionVector()
    {
        return direction switch
        {
            MoveDirection2D.Right => Vector2.right,
            MoveDirection2D.Left => Vector2.left,
            MoveDirection2D.Up => Vector2.up,
            MoveDirection2D.Down => Vector2.down,
            _ => Vector2.zero
        };
    }


    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.SetParent(null);
    }


}
