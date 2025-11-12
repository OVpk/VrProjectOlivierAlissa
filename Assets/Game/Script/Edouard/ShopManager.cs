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
    [SerializeField] public List<Items>  shopItems = new List<Items>();
    [SerializeField] InventoryManager inventoryManagerReference;
    [SerializeField] ItemUI ItemUIReference;
    [SerializeField] Player moneyRef;
    #endregion

    private void Awake()
    {
        ActionManager.endOfRound += ActiveShop;
    }

    private void OnDestroy()
    {
        ActionManager.endOfRound -= ActiveShop;
    }

    private void ActiveShop()
    {
        GameManager.instance.CurrentGameState = GameState.InShop;
        gameObject.SetActive(true);
    }

    void Start()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            ItemUIReference.itemsReference = shopItems[i]; ItemUIReference.shopManagerReference = this;
            ItemUIReference.itemID = i;
            Instantiate(shopItemUIPrefab, shopUI.transform);
        }
        gameObject.SetActive(false);
    }

    public void Buy(int _itemId)
    {
        Items itemsReference = shopItems[_itemId];

        if (moneyRef.chipNum >= itemsReference.price)
        {
            moneyRef.chipNum -= itemsReference.price;

            if (inventoryManagerReference.itemsDictionary.ContainsKey(itemsReference))
                inventoryManagerReference.itemsDictionary[itemsReference] += 1;
            else
                inventoryManagerReference.itemsDictionary[itemsReference] = 1;
            
            inventoryManagerReference.DictionaryToLists();
        }
        else
        {
        }
    }

}