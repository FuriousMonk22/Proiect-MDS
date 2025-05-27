using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public int price = 5;
    public string itemName;
    public Player player;
    public Sprite icon; // <- AICI e proprietatea care lipseste


    public void BuyItem()
    {
        if (player.TryBuy(price))
        {
            Debug.Log($"Ai cumpărat: {itemName}");
            // Adaugă item in inventar etc.
        }
    }
}
