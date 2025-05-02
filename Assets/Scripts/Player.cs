using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float acceleration = 10f;

    [Header("Jump Control")]
    public float lowGravityScale = 0.5f;   // While holding jump and rising
    public float fallGravityScale = 2f;    // When falling or not holding jump
    public float defaultGravityScale = 1f; // Normal gravity

    private Rigidbody2D plr;
    private bool isGrounded;
    private float currentVelocityX;
    private bool jumpRequested = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private void Awake()
    {
        plr = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        healthBar.SetHealth((float)currentHealth / (float)maxHealth);
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Horizontal movement
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput = 1f;

        float targetVelocityX = moveInput * moveSpeed;
        currentVelocityX = Mathf.Lerp(plr.linearVelocity.x, targetVelocityX, acceleration * Time.fixedDeltaTime);
        plr.linearVelocity = new Vector2(currentVelocityX, plr.linearVelocity.y);

        // Apply jump
        if (jumpRequested)
        {
            plr.linearVelocity = new Vector2(plr.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }

        // Gravity control for variable jump height
        if (plr.linearVelocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            // Rising and jump held: floatier jump
            plr.gravityScale = lowGravityScale;
        }
        else if (plr.linearVelocity.y < 0)
        {
            // Falling: increase gravity for snappier fall
            plr.gravityScale = fallGravityScale;
        }
        else
        {
            // Default gravity (e.g., rising but jump not held)
            plr.gravityScale = defaultGravityScale;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // If using image-based health bar
        // healthBar.SetHealthImageFill((float)currentHealth / maxHealth);
    }
}
