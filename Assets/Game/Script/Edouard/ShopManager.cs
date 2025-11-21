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
    [SerializeField] private TextMeshProUGUI textMoney;

    public List<Item> items = new List<Item>();
    #endregion

    private void OnEnable()
    {
        textMoney.text = moneyRef.money.ToString();
    }

    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject newItemUI = Instantiate(shopItemUIPrefab, shopUI.transform);

            ItemUI ui = newItemUI.GetComponent<ItemUI>();

            ui.itemsReference = items[i].itemData;
            ui.shopManagerReference = this;
            ui.itemID = i;

            items[i].id = i;

            if (!items[i].isUnlock)
                ui.Lock();
        }
    }



    public void Buy(int _itemId)
    {
        Item lItem = items[_itemId];

        if (!lItem.isUnlock || lItem.bought)
            return;
        ItemData itemsReference = items[_itemId].itemData;

        if (moneyRef.money >= itemsReference.price)
        {
            moneyRef.money -= itemsReference.price;
            lItem.bought = true;
            lItem.gameObject.SetActive(true);
        }
    }

}