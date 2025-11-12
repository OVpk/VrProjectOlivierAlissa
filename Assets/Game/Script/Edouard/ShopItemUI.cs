using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] String itemName;
    [SerializeField] TMP_Text itemPrice;
    [SerializeField] Image itemIcon;
    [SerializeField] Button buyButton;
    
    public Items itemsReference; 
    public ShopManager  shopManagerReference; 
    public int itemID;
    
    private void Awake()
    {
        Debug.Log($"{itemName} Awakened");
        itemName = itemsReference.name;
        itemPrice.text = itemsReference.price.ToString();
        itemIcon.sprite = itemsReference.icon;
        
        buyButton.onClick.AddListener(()=>shopManagerReference.Buy(itemID));
    }
}
