// Coin.cs (on your Coin prefab or object)
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Try to get the Player component
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (player != null) // Ensure the Player script exists
            {
                player.AddCoin(1); // Increment coins
                Destroy(gameObject); // Destroy the coin
            }
            else
            {
                Debug.LogError("Player script missing on collided object!");
            }
        }
    }
}