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
            GameObject lNewItemUI = Instantiate(shopItemUIPrefab, shopUI.transform);
            ItemUI lUi = lNewItemUI.GetComponent<ItemUI>();
            lUi.itemsReference = items[i].itemData;
            lUi.shopManagerReference = this;
            lUi.itemID = i;

            items[i].id = i;
            items[i].InitChallenge();

            if (!items[i].isUnlock)
                lUi.Lock();
        }
    }

    public void Buy(int pItemId)
    {
        Item lItem = items[pItemId];

        if (!lItem.isUnlock || lItem.bought)
            return;

        ItemData lItemsReference = items[pItemId].itemData;

        if (moneyRef.money >= lItemsReference.price)
        {
            moneyRef.money -= lItemsReference.price;
            textMoney.text = moneyRef.money.ToString();
            lItem.bought = true;
            lItem.gameObject.SetActive(true);
        }
    }

}