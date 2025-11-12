using UnityEngine;

[CreateAssetMenu(fileName = "TutoScriptable", menuName = "Scriptable Objects/TutoScriptable")]
public class TutoScriptable : ScriptableObject
{
    [field:SerializeField] public string title { get; private set; }
    [field:SerializeField] public string description { get; private set;}
    [field:SerializeField] public GameObject image { get; private set; }
}
