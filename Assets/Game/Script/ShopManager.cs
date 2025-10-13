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
        if (inventoryReference.money >= shopItems[_itemId].price)
        {
            inventoryReference.money -= shopItems[_itemId].price;
            inventoryReference.items.Add(1,shopItems[_itemId]);
            //TODO Fix this : .Add(1,shopItems[_itemId])
        }
    }
}
