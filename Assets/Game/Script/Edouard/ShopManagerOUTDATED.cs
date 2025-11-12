using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManagerOUTDATED : MonoBehaviour
{
    #region Unity Variables
    [SerializeField] GameObject shopMenu;
    [SerializeField] InventoryManager inventoryManagerReference;
    [SerializeField] List<Items>  shopItems = new List<Items>();
    [SerializeField] List<TMP_Text> shopButtonText = new List<TMP_Text>();
    [SerializeField] List<Image> shopImages = new List<Image>();
    [SerializeField] List<Button> shopButtons = new List<Button>();
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

        if (inventoryManagerReference.money >= item.price)
        {
            inventoryManagerReference.money -= item.price;
            Debug.Log("Item Bought");

            if (inventoryManagerReference.itemsDictionary.ContainsKey(item))
                inventoryManagerReference.itemsDictionary[item] += 1;
            else
                inventoryManagerReference.itemsDictionary[item] = 1;
            
            inventoryManagerReference.DictionaryToLists();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

}