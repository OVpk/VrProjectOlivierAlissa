using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Placer l'objet là où vous voulez faire spawn les models des Items")]
    [SerializeField] GameObject objectSpawn;

    #region Lists
    [SerializeField] List<Items> itemList = new List<Items>();
    [SerializeField] List<int> itemInt = new List<int>();
    
    public Dictionary<Items, int> itemsDictionary = new Dictionary<Items, int>(); 
    
    #endregion
    
    public int money;
    
    #endregion


    public void Spawn( Items item )
    {
        Instantiate(item.model, objectSpawn.transform.position, Quaternion.identity);
    }


    public void DictionaryToLists() //this is usd to be able to see the dictionary in the inspector
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