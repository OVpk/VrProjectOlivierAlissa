using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] String itemName;
    [SerializeField] TMP_Text itemPrice;
    [SerializeField] Image itemIcon;
    [SerializeField] Button buyButton;

    public ItemData itemsReference;
    public ShopManager shopManagerReference;
    public int itemID;

    private void Awake()
    {
        itemName = itemsReference.itemName;
        itemPrice.text = itemsReference.price.ToString();
        itemIcon.sprite = itemsReference.icon;

        buyButton.onClick.AddListener(() => shopManagerReference.Buy(itemID));
    }
}
