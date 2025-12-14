using UnityEngine;

// hésitez pas à sécuriser vos wrappers, là si quelqu'un bricole en runtime vous perdez vos datas

[CreateAssetMenu(menuName = "Scriptable Objects/ItemData", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public string itemName { get; private set; }
    [field: SerializeField] public int price { get; private set; }
    [field: SerializeField] public Sprite icon { get; private set; }
    [field: SerializeField] public string DescriptionItem { get; private set; }
}
