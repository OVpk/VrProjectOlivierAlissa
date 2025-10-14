using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<Items, int> items = new Dictionary<Items, int>();
    public int money;
}