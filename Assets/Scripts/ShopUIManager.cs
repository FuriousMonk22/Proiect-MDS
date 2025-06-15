using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopUIManager : MonoBehaviour
{
    public GameObject itemUIPrefab;
    public Transform itemContainer;
    public List<ShopItem> shopItems;

    private void OnEnable()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItem item in shopItems)
        {
            // Creeaza randul în UI
            GameObject row = Instantiate(itemUIPrefab, itemContainer);

            // Seteaza icon-ul
            Image icon = row.transform.Find("ItemIcon").GetComponent<Image>();
            if (item.icon != null)
            {
                icon.sprite = item.icon;
            }

            // Seteaza pretul
            TMP_Text priceText = row.transform.Find("PriceText").GetComponent<TMP_Text>();
            priceText.text = $"{item.price} G - {item.name}";

            // Adauga functionalitatea de cumparare pe butonul principal
            Button btn = row.GetComponent<Button>();
            if (btn != null)
            {
                ShopItem currentItem = item;
                btn.onClick.AddListener(() => currentItem.BuyItem());
            }

        }
    }
}
