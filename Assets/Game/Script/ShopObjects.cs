using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopObject", menuName = "Scriptable Objects/ShopObjects")]
public class ShopObjects : ScriptableObject
{
    public string name;
    public string price;
    public Sprite icon;
}
