using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; // Karakterin ayaklar�n�n alt�na bo� GameObject
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask groundLayer; // Inspector'dan "Ground" layer'�n� se�in
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float riseMultiplier = 2.5f;



    [SerializeField] private TrailRenderer _tr;
    private SpriteRenderer _spriteRenderer;
    public Rigidbody2D _rb;
    public Animator _animator;

    [Header("Movement Settings")]
    [Range(0f, 20f)]
    [SerializeField] public float jumpForce = 5f;

    [Range(0f, 20f)]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference dash;

    [SerializeField] private bool _isGrounded;
    private Vector2 _moveDirection;



    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);

        if (_rb.linearVelocity.y > 0f)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.deltaTime;
        }

        else if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
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
        _animator = GetComponent<Animator>();   
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }


    }


    void Move()
    {
        _moveDirection = move.action.ReadValue<Vector2>();

        // Yerdeyken tam kontrol, havadayken s�n�rl� kontrol
        float airControlMultiplier = _isGrounded ? 1f : 0.9f;

        float targetVelocityX = _moveDirection.x * moveSpeed * airControlMultiplier;

        _rb.linearVelocity = new Vector2(
            targetVelocityX,
            _rb.linearVelocity.y
        );

        // Sprite y�n�
        if (_moveDirection.x < 0)
            _spriteRenderer.flipX = true;
        else if (_moveDirection.x > 0)
            _spriteRenderer.flipX = false;
    }


    void Jump()
    {
        if (jump.action.WasPressedThisFrame() && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            _animator.SetTrigger("Jump");
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

}
