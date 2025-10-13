using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<int, Items> items = new Dictionary<int, Items>();
    
    public int money; 
}