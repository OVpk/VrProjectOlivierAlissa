using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Variables

    #region Lists
    [SerializeField] List<ItemData> itemList = new List<ItemData>();
    [SerializeField] List<int> itemInt = new List<int>();
    
    public Dictionary<ItemData, int> itemsDictionary = new Dictionary<ItemData, int>(); 
    #endregion
    
    public int money;
    
    #endregion

    public void DictionaryToLists()
    {
        itemInt.Clear();
        itemList.Clear();
        foreach (KeyValuePair<ItemData, int> lItem in itemsDictionary)
        {
            itemList.Add(lItem.Key);
            itemInt.Add(lItem.Value);
        }
    }
}