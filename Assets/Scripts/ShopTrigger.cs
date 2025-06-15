using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(false);
        }
    }
}
