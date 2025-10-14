using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Android;
using UnityEngine.Events;

public class ShopManager : MonoBehaviour
{
    #region Unity Variables
    [SerializeField] GameObject shopMenu;
    [SerializeField] List<Items>  shopItems = new List<Items>();
    [SerializeField] List<TMP_Text> shopButtonText = new List<TMP_Text>();
    [SerializeField] List<Image> shopImages = new List<Image>();
    [SerializeField] List<Button> shopButtons = new List<Button>();
    Inventory inventoryReference;
    #endregion

    void Start()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopButtonText[i].text = shopItems[i].price.ToString();
            shopImages[i].sprite = shopItems[i].icon;
        }
    }

    void Awake()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            var i1 = i;
            shopButtons[i].onClick.AddListener(()=>Buy(i1));
        }
    }

    void Buy(int _itemId)
    {
        Items item = shopItems[_itemId];

        if (inventoryReference.money >= item.price)
        {
            inventoryReference.money -= item.price;
            Debug.Log("Item Bought");

            if (inventoryReference.items.ContainsKey(item))
                inventoryReference.items[item] += 1;
            else
                inventoryReference.items[item] = 1;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

}
