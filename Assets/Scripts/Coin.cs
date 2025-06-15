using UnityEngine;

public class Coin : MonoBehaviour
{
    [Tooltip("How many coins this object gives when collected")]
    public int coinValue = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (player != null)
            {
                player.AddCoin(coinValue);  // Use coinValue here
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Player script missing on collided object!");
            }
        }
    }
}
