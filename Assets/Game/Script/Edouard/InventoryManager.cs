using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Placer les objets là où vous voulez faire spawn les models des Items")]
    [SerializeField] GameObject[] objectSpawn;

    #region Lists
    [SerializeField] List<Items> itemList = new List<Items>();
    [SerializeField] List<int> itemInt = new List<int>();
    
    public Dictionary<Items, int> itemsDictionary = new Dictionary<Items, int>(); 
    
    #endregion
    
    public int money;
    
    #endregion


    public void Spawn(Items item, int _itemId)
    {
        /*Vector2 randomPos = new Vector2(Random.Range(objectSpawn[_itemId].transform.position.z - 0.1f,
            objectSpawn[_itemId].transform.position.z + 0.1f), 
            Random.Range(objectSpawn[_itemId].transform.position.y - 0.1f,
                objectSpawn[_itemId].transform.position.y + 0.1f));*/
        for (int i = 0; i < itemList.Count; i++)
        {
            if (item.name == itemList[i].name)
            {
                Instantiate(item.model,objectSpawn[i].transform.position /*randomPos*/, 
                    Quaternion.identity);
            }
        }
    }


    public void DictionaryToLists() //this is used to be able to see the dictionary in the inspector
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