using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private String itemName;
    [SerializeField] private TMP_Text itemPrice;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject lockImage;

    public ItemData itemsReference;
    public ShopManager shopManagerReference;
    public int itemID;

    private void Awake()
    {
        ActionManager.Unlock += Unlock;

        itemName = itemsReference.itemName;
        itemPrice.text = itemsReference.price.ToString();
        itemIcon.sprite = itemsReference.icon;
        lockImage.SetActive(itemsReference.locked);

        buyButton.onClick.AddListener(() => shopManagerReference.Buy(itemID));
    }

    private void Unlock(int pID)
    {
        if (pID == itemID)
            lockImage.SetActive(false);
    }
}
