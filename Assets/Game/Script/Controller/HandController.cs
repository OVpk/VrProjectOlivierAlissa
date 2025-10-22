using DG.Tweening;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    [SerializeField] private CardData[] cards;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private LayerMask cardValideLayers;
    [SerializeField] private EnumHand currentHand;
    [SerializeField] private GameObject anchor;
    [SerializeField] private Transform tableTarget;
    [SerializeField] private float throwInfluence = 0.4f; 
    [SerializeField] private float throwForce = 8f;

    private Collider cardCollider;
    private GameObject cardInHand;
    private Rigidbody cardRB;
    private DroppedCard cardDropped;
    private int cardIndex = 0;
    private Vector3 lastAnchorPos;
    private Vector3 anchorVelocity;

    private void OnEnable()
    {
        ActionManager.spawnCard += SpawnCard;
        ActionManager.removeCard += ReleaseCard;
        ActionManager.changeCard += ChangeCard;
        ActionManager.resetCardInHand += DeactivateCard;
    }

    private void OnDestroy()
    {
        ActionManager.spawnCard -= SpawnCard;
        ActionManager.removeCard -= ReleaseCard;
        ActionManager.changeCard -= ChangeCard;
        ActionManager.resetCardInHand -= DeactivateCard;
    }

    private void Start()
    {
        cardInHand = Instantiate(cardPrefab, anchor.transform);
        cardRB = cardInHand.GetComponent<Rigidbody>();
        cardDropped = cardInHand.GetComponent<DroppedCard>();
        cardCollider = cardInHand.GetComponent<Collider>();
        cardDropped.isPlayer = true;
        ResetCardPosition();
        cardInHand.SetActive(false);
    }

    private void Update()
    {
        if (!cardInHand.activeInHierarchy || cardDropped.IsDropped)
            return;

        Vector3 currentPos = anchor.transform.position;
        anchorVelocity = (currentPos - lastAnchorPos) / Time.deltaTime;
        lastAnchorPos = currentPos;

    }

    private void ResetCardPosition()
    {
        cardInHand.transform.parent = anchor.transform;
        cardDropped.IsDropped = false;
        cardInHand.transform.localPosition = Vector3.zero;
        cardInHand.transform.rotation = Quaternion.identity;

        cardRB.linearVelocity = Vector3.zero;
        cardRB.angularVelocity = Vector3.zero;
        cardRB.useGravity = false;

    }

    private void SpawnCard(EnumHand pHand)
    {
        if (currentHand != pHand)
            return;

        ResetCardPosition();
        cardInHand.SetActive(true);
        cardCollider.enabled = false;
        cardDropped.spriteDisplayer.sprite = cards[cardIndex].visual;

    }

    private void ChangeCard(EnumHand pHand)
    {
        if (currentHand != pHand || !cardInHand.activeInHierarchy || cardDropped.IsDropped)
            return;

        cardIndex = (cardIndex + 1) % cards.Length;
        SpawnCard(pHand);
    }

    private void ReleaseCard(EnumHand pHand)
    {
        if (currentHand != pHand || !cardInHand.activeInHierarchy)
            return;

        cardInHand.transform.parent = null;

        cardRB.useGravity = true;
        cardRB.isKinematic = false;
        cardCollider.enabled = true;

        float handSpeed = anchorVelocity.magnitude;
        

        if (handSpeed > 0.35f)
        {
            Vector3 toTable = (tableTarget.position - anchor.transform.position).normalized;
            Vector3 handDir = anchorVelocity.normalized;
            Vector3 finalDir = Vector3.Slerp(toTable, handDir, throwInfluence).normalized;
            cardRB.AddForce(finalDir * throwForce, ForceMode.VelocityChange);
        }

        cardRB.DORotate(new Vector3(90f, cardRB.transform.rotation.eulerAngles.y, cardRB.transform.rotation.eulerAngles.z), 0.2f);

        cardDropped.IsDropped = true;
        cardDropped.cardData = cards[cardIndex].Instance(CardState.Play);
    }

    private void DeactivateCard() => cardInHand.SetActive(false);
}
