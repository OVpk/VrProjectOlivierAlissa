using DG.Tweening;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    [SerializeField] private CardData[] cards;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private LayerMask cardValideLayers;
    [SerializeField] private EnumHand currentHand;
    [SerializeField] private GameObject anchor;

    private GameObject cardInHand;
    private Rigidbody cardRB;
    private DroppedCard cardDropped;
    private int cardIndex = 0;

    private void OnEnable()
    {
        ActionManager.spawnCard += SpawnCard;
        ActionManager.removeCard += ReleaseCard;
        ActionManager.changeCard += ChangeCard;
    }

    private void OnDestroy()
    {
        ActionManager.spawnCard -= SpawnCard;
        ActionManager.removeCard -= ReleaseCard;
        ActionManager.changeCard -= ChangeCard;
    }

    private void Start()
    {
        cardInHand = Instantiate(cardPrefab, anchor.transform);
        cardRB = cardInHand.GetComponent<Rigidbody>();
        cardDropped = cardInHand.GetComponent<DroppedCard>();
        cardDropped.isPlayer = true;
        ResetCardPosition();
        cardInHand.SetActive(false);
    }

    private void ResetCardPosition()
    {
        cardInHand.transform.parent = anchor.transform;
        cardDropped.IsDropped = false;
        cardInHand.transform.localPosition = Vector3.zero;
        cardInHand.transform.rotation = Quaternion.identity;
        cardRB.isKinematic = true;
    }

    private void SpawnCard(EnumHand pHand)
    {
        if (currentHand != pHand)
            return;

        ResetCardPosition();
        cardInHand.SetActive(true);
        cardDropped.spriteDisplayer.sprite = cards[cardIndex].visual;

    }

    private void ChangeCard(EnumHand pHand)
    {
        if (currentHand != pHand)
            return;

        cardIndex = (cardIndex +1) % cards.Length;
        SpawnCard(pHand);
    }

    private void ReleaseCard(EnumHand pHand)
    {
        if (currentHand != pHand || cardInHand == null)
            return;

        RaycastHit hit;
        if (Physics.Raycast(cardInHand.transform.position, Vector3.down, out hit, Mathf.Infinity, cardValideLayers))
        {
            cardInHand.transform.parent = null;
            cardRB.isKinematic = false;
            cardRB.DORotate(new Vector3(90f, cardRB.transform.rotation.eulerAngles.y, cardRB.transform.rotation.eulerAngles.z), 0.2f);

            cardDropped.IsDropped = true;
            cardDropped.cardData = cards[cardIndex].Instance(CardState.Play);
        }
        else
        {
            cardInHand.SetActive(false);
        }
    }
}
