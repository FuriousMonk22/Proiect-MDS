using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public Rigidbody2D rb;
    public float lifetime = 1.5f;

    void Start()
    {
        rb.linearVelocity = InitialVelocity;
        Destroy(gameObject, lifetime); // Destroy the object after 1 second    
    }

    void Update()
    {
        
    }
}
