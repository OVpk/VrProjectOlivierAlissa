using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private String itemName;
    [SerializeField] private TMP_Text itemPrice;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI textDescription;
    public ItemData itemsReference;
    public ShopManager shopManagerReference;
    public int itemID;
    private bool isLocked;

    private void Awake()
    {
        ActionManager.unlock += Unlock;
        buyButton.onClick.AddListener(() => shopManagerReference.Buy(itemID));
    }

    private void Start()
    {
        itemName = itemsReference.itemName;
        itemPrice.text = itemsReference.price.ToString();
        itemIcon.sprite = itemsReference.icon;
    }
    public void Lock()
    {
        lockImage.SetActive(true);
        textDescription.text = itemsReference.DescriptionItem;
        isLocked = true;

    }
    private void Unlock(int pID)
    {
        if (pID == itemID)
        {
            lockImage.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData pEventData)
    {
        if (!isLocked)
            return;
        description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pEventData)
    {
        if (!isLocked)
            return;
        description.SetActive(false);
    }
}
