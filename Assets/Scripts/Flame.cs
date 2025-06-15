using UnityEngine;

public class Flame : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;

    protected Rigidbody2D rb;

    protected void Start()
    {
        Destroy(gameObject, lifeTime); // dispare după ceva timp

        // setare unghi
        rb = GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void FixedUpdate()
    {
        Vector2 direction = GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Debug.DrawLine(transform.position, transform.position + (Vector3)(direction * 0.5f), Color.red);
        CustomUpdate();
    }
    
    protected virtual void CustomUpdate()
    {
        // Your specific logic here
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("CurrentHealth") > 0)
                PlayerPrefs.SetInt("CurrentHealth", PlayerPrefs.GetInt("CurrentHealth") - damage);
        }
        Destroy(gameObject);
    }
}