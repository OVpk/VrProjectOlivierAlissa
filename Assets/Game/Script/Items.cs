using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public string name;
    public int price;
    public Sprite icon;
}
