using UnityEngine;

public class FlameHoming : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;

    void Start()
    {  
        Destroy(gameObject, lifeTime); // dispare după ceva timp
    }

    void Update()
    {
        Vector2 direction = GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Debug.DrawLine(transform.position, transform.position + (Vector3)(direction * 0.5f), Color.red);

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