using UnityEngine;

public class FlameHoming : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;
    public float knockbackForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // auto-destroy
    }

    void Update()
    {
        if (rb != null)
        {
            Vector2 direction = rb.linearVelocity.normalized;
            Debug.DrawLine(transform.position, transform.position + (Vector3)(direction * 0.5f), Color.red);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage
            if (PlayerPrefs.GetInt("CurrentHealth") > 0)
                PlayerPrefs.SetInt("CurrentHealth", PlayerPrefs.GetInt("CurrentHealth") - damage);

            // Apply knockback
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
