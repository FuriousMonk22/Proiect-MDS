using UnityEngine;

public class Flame : MonoBehaviour
{
    public int damage = 13;
    public float lifeTime = 12.5f;
    public float homingSpeed = 5f;      // Speed at which projectile moves
    public float homingStrength = 0.5f;   // How quickly it turns towards the player

    private Rigidbody2D rb;
    private Transform playerTransform;

    void Start()
    {  
        Destroy(gameObject, lifeTime);

        rb = GetComponent<Rigidbody2D>();

        // Find player by tag or assign it some other way
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Direction from projectile to player
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Current velocity direction
            Vector2 currentVelocity = rb.linearVelocity.normalized;

            // Interpolate between current direction and direction to player to create smooth homing
            Vector2 newDirection = Vector2.Lerp(currentVelocity, directionToPlayer, homingStrength * Time.fixedDeltaTime).normalized;

            // Set velocity to the new direction times speed
            rb.linearVelocity = newDirection * homingSpeed;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, 45 * Time.deltaTime);
        // Optional: draw debug line showing direction
        Debug.DrawLine(transform.position, transform.position + (Vector3)(rb.linearVelocity.normalized * 0.5f), Color.red);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(PlayerPrefs.GetInt("CurrentHealth") > 0)
                PlayerPrefs.SetInt("CurrentHealth", PlayerPrefs.GetInt("CurrentHealth") - damage);
        }
        Destroy(gameObject);
    }
}
