using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float riseMultiplier = 2.5f;

    [Header("Jump Buffer")]
    [SerializeField] private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;


    private SpriteRenderer _spriteRenderer;
    public Rigidbody2D _rb;
    public Animator _animator;

    [Header("Movement Settings")]
    [Range(0f, 100f)]
    [SerializeField] public float jumpForce = 5f;

    [Range(0f, 20f)]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;

    [SerializeField] private bool _isGrounded;
    private Vector2 _moveDirection;



    void FixedUpdate()
    {
        CheckGround();
        ApplyJumpGravity();
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }
    void Start()
    {
        Time.timeScale = 1.3f;
        _animator = GetComponent<Animator>();   
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        TryJump();
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }


        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main Menu Scene");
        }

        if (jump.action.WasPressedThisFrame())
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;


        if (_isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }

    void Move()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
        
        
        float airControlMultiplier = _isGrounded ? 1f : 0.9f;

        float targetVelocityX = _moveDirection.x * moveSpeed * airControlMultiplier;

        _rb.linearVelocity = new Vector2(
            targetVelocityX,
            _rb.linearVelocity.y
        );

        _animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));

        // Sprite y�n�
        if (_moveDirection.x < 0)
            _spriteRenderer.flipX = true;
        else if (_moveDirection.x > 0)
            _spriteRenderer.flipX = false;
    }

    void TryJump()
    {
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }
    }
    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        _animator.SetTrigger("Jump");
    }

    void CheckGround()
    {
        _isGrounded = Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0f,
            groundLayer
        );
    }

    void ApplyJumpGravity()
    {
        if (_rb.linearVelocity.y > 0f)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else if (_rb.linearVelocity.y < 0f)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

}
