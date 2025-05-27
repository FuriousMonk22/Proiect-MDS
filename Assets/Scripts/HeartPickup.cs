using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Heal(healAmount);
                Destroy(gameObject); // elimină inima după colectare
            }
        }
    }
}
