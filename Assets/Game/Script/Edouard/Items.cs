using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public string name;
    public int price;
    [Tooltip("objectIcon for the shop")]
    public Sprite icon;
    [Tooltip("3D model for spawn")]
    public GameObject model;

    public void UseItem()
    {
        Debug.Log($"Item {name} was used");
    }
}
