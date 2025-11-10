using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Variables

    #region Lists
    [SerializeField] List<Items> itemList = new List<Items>();
    [SerializeField] List<int> itemInt = new List<int>();
    
    public Dictionary<Items, int> itemsDictionary = new Dictionary<Items, int>(); 
    #endregion
    
    public int money;
    
    #endregion

    public void DictionaryToLists()
    {
        itemInt.Clear();
        itemList.Clear();
        foreach (KeyValuePair<Items, int> item in itemsDictionary)
        {
            itemList.Add(item.Key);
            itemInt.Add(item.Value);
        }
    }
}