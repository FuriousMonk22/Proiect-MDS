using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D plr;
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private void Awake()
    {
        plr = GetComponent<Rigidbody2D>();
    }

    private void Update()
{
    // Horizontal movement
    float moveInput = 0f;

    if (Input.GetKey(KeyCode.A))
        moveInput = -1f;
    else if (Input.GetKey(KeyCode.D))
        moveInput = 1f;

    plr.linearVelocity = new Vector2(moveInput * moveSpeed, plr.linearVelocity.y);

    // Ground check
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    // Jumping
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
        plr.linearVelocity = new Vector2(plr.linearVelocity.x, jumpForce);
    }
    }
}
