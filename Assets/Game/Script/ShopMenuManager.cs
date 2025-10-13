using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    #region Unity Variables
    [SerializeField] GameObject shopMenu;
    [SerializeField] List<Button> shopButtons = new List<Button>();
    
    List<ShopObjects>  shopObjects = new List<ShopObjects>();
    #endregion
}
