using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [field : SerializeField] public Image spriteDisplayer {  get;  private set; }

    public bool isPlayer;
}
