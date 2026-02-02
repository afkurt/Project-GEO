using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SpikeGroup : InteractableBase
{
    [Header("Kill")]
    [SerializeField] private bool killOnTouch = true;

    [Header("Movement")]
    [SerializeField] private bool isMoving = false;
    [SerializeField] private MoveDirection2D direction;
    [SerializeField] private float distance = 2f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private bool loop = true;

    [Header("Curve")]
    [SerializeField]
    private AnimationCurve moveCurve;
        

    private Vector2 startPos;
    private Tween moveTween;
    private bool triggered;

    private void Start()
    {
        startPos = transform.position;

        if (isMoving)
            StartMovement();
    }

    public override void BeginInteraction()
    {
        if (triggered) return;
        triggered = true;

        if (killOnTouch)
            ReloadScene();
    }

    public override void EndInteraction()
    {
        
    }

    public void StartMovement()
    {
        Vector2 targetPos = startPos + GetDirectionVector() * distance;

        moveTween = transform.DOMove(targetPos, duration)
            .SetEase(moveCurve)
            .SetAutoKill(false);

        if (loop)
            moveTween.SetLoops(-1, LoopType.Yoyo);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
