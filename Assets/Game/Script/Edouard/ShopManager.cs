using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    #region Unity Variables
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject shopItemUIPrefab;
    [SerializeField] InventoryManager inventoryManagerReference;
    [SerializeField] ItemUI ItemUIReference;
    [SerializeField] Player moneyRef;
    [SerializeField] public List<Item> items = new List<Item>();
    #endregion

    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            ItemUIReference.itemsReference = items[i].itemData; 
            ItemUIReference.shopManagerReference = this;
            ItemUIReference.itemID = i;
            items[i].id = i;
            Instantiate(shopItemUIPrefab, shopUI.transform);
        }
    }

    public void Buy(int _itemId)
    {
        Item lItem = items[_itemId];

        Debug.Log("is item unlocked :" + lItem.isUnlock);
        if (!lItem.isUnlock || lItem.bought)
            return;
        ItemData itemsReference = items[_itemId].itemData;

        if (moneyRef.chipNum >= itemsReference.price)
        {
            moneyRef.chipNum -= itemsReference.price;
            lItem.bought = true;
            lItem.gameObject.SetActive(true);
        }
    }

}