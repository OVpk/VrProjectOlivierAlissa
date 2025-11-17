using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Unity Variables
    [Tooltip("Ajouter UI du shop")]
    [SerializeField] GameObject shopUI;
    [Tooltip("Ajouter Prefab UI pour objets")]
    [SerializeField] GameObject shopItemUIPrefab;
    [Tooltip("Ajouter les sciptableObjects de chaque objet")]
    [SerializeField] public List<Items>  shopItems = new List<Items>();
    [Tooltip("Ajouter script/gameObject avec InventoryManager")]
    [SerializeField] InventoryManager inventoryManagerReference;
    [Tooltip("Ajouter script/gameObject avec ShopItemUI")]
    [SerializeField] ShopItemUI shopItemUIReference;
    #endregion

    void Start()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItemUIReference.itemsReference = shopItems[i]; shopItemUIReference.shopManagerReference = this;
            shopItemUIReference.itemID = i;
            Instantiate(shopItemUIPrefab, shopUI.transform);
        }
    }

    public void Buy(int _itemId)
    {
        Items itemsReference = shopItems[_itemId];

        if (inventoryManagerReference.money >= itemsReference.price)
        {
            inventoryManagerReference.money -= itemsReference.price;
            Debug.Log("Item Bought");

            if (inventoryManagerReference.itemsDictionary.ContainsKey(itemsReference))
                inventoryManagerReference.itemsDictionary[itemsReference] += 1;
            else
                inventoryManagerReference.itemsDictionary[itemsReference] = 1;
            
            inventoryManagerReference.DictionaryToLists(); //to see the dictionary in inspector
            inventoryManagerReference.Spawn(itemsReference, _itemId);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

}