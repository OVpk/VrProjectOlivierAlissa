using UnityEngine;

[CreateAssetMenu(fileName = "ShopObjects", menuName = "Scriptable Objects/ShopObjects")]
public class ShopObjects : ScriptableObject
{
    public string name;
    public int price;
    public Sprite icon;
}
