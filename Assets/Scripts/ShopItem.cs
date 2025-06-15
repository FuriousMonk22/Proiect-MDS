using UnityEngine;
using TMPro;

public enum ItemType { HP, Sword, Jump, Speed }

public class ShopItem : MonoBehaviour
{
    [Header("Config")]
    public ItemType itemType;
    public int price = 5;
    public Player player;

    [Header("UI")]
    public TextMeshProUGUI stockText;
    public Sprite icon; // <- variabila folosita in script-ul ShopUIManager.cs (nefolosita aici)

    // un key diferit pentru fiecare item
    private string PrefKey => $"stock_{itemType}";

    private void Awake()
    {
        // initializează stocul la 0 doar dacă nu există deja
        if (!PlayerPrefs.HasKey(PrefKey))
            PlayerPrefs.SetInt(PrefKey, 0);
        UpdateStockUI();
    }

    private void Update()
    {
        // pentru UI e ok Update
        UpdateStockUI();
    }

    private void UpdateStockUI()
    {
        int stock = PlayerPrefs.GetInt(PrefKey);
        stockText.text = stock.ToString();
    }

    public void BuyItem()
    {
        if (player.TryBuy(price))
        {
            int stock = PlayerPrefs.GetInt(PrefKey) + 1;
            PlayerPrefs.SetInt(PrefKey, stock);
            Debug.Log($"Ai cumpărat: {itemType} (acum ai {stock})");
        }
    }

    public void UseItem()
    {
        int stock = PlayerPrefs.GetInt(PrefKey);
        if (stock <= 0) return;

        // consumi unul
        stock--;
        PlayerPrefs.SetInt(PrefKey, stock);
        Debug.Log($"Ai folosit: {itemType} (rămân {stock})");

        // logica per item:
        switch (itemType)
        {
            case ItemType.HP:
                int curHp = PlayerPrefs.GetInt("CurrentHealth");
                PlayerPrefs.SetInt("CurrentHealth", curHp < 75 ? curHp + 25 : 100);
                break;

            case ItemType.Sword:
                player.attackDamage += 5;
                break;

            case ItemType.Jump:
                player.jumpForce += 1f;
                break;

            case ItemType.Speed:
                player.moveSpeed += 2f;
                break;
        }
    }
}
