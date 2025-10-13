using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenuManager : MonoBehaviour
{
    #region Unity Variables
    [SerializeField] GameObject shopMenu;
    [SerializeField] List<ShopObjects>  shopObjects = new List<ShopObjects>();
    [SerializeField] List<TMP_Text> shopButtonText = new List<TMP_Text>();
    [SerializeField] List<Image> shopImages = new List<Image>();
    #endregion

    void Start()
    {
        for (int i = 0; i < shopObjects.Count; i++)
        {
            shopButtonText[i].text = shopObjects[i].price;
            shopImages[i].sprite = shopObjects[i].icon;
        }
        
    }
}
