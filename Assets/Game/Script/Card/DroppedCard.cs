using UnityEngine;
using UnityEngine.UI;

public class DroppedCard : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public Image spriteDisplayer;
    public CardData cardData;

    public bool isPlayer;

    private bool isDropped;
    public bool IsDropped;

    private void OnEnable()
    {
        ActionManager.destroyAllCard += DeleteAllCard;
    }

    private void Start()
    {
        if (!isPlayer)
            rb.useGravity = false;
    }

    private void OnDisable()
    {
        ActionManager.destroyAllCard -= DeleteAllCard;
    }


    private void DeleteAllCard()
    {
        if (!isDropped)
            return;

        gameObject.SetActive(false);
    }
}
