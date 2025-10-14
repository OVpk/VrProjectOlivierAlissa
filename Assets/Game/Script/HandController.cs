using DG.Tweening;
using System.ComponentModel.Design;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefab;
    [SerializeField] private LayerMask cardValideLayers;
    [SerializeField] private EnumHand currentHand;
    [SerializeField] private GameObject anchor;

    private GameObject cardInHand;
    private Rigidbody cardRB;
    private GameObject LastCard;
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

    private void SpawnCard(EnumHand pHand)
    {
        if (currentHand != pHand)
            return;
        if (cardInHand == null)
        {
            cardInHand = Instantiate(cardPrefab[cardIndex], anchor.transform);
            cardInHand.transform.localPosition = Vector3.zero;
            cardRB = cardInHand.GetComponent<Rigidbody>();
        }
    }

    private void ChangeCard(EnumHand pHand)
    {
        if (currentHand != pHand)
            return;
        if (cardInHand != null)
        {
            Destroy(cardInHand.gameObject);
            cardInHand = null;
            if (cardIndex == cardPrefab.Length - 1)
                cardIndex = 0;
            else
                cardIndex += 1;
                     
            SpawnCard(pHand);
        }
    }

    private void ReleaseCard(EnumHand pHand)
    {
        if (currentHand != pHand || cardInHand == null)
            return;

        RaycastHit hit;
        if (Physics.Raycast(cardInHand.transform.position, Vector3.down, out hit, Mathf.Infinity, cardValideLayers))
        {
            if (LastCard != null)
            {
                Destroy(LastCard);
                LastCard = null;
            }
            cardRB.isKinematic = false;
            cardRB.DORotate(new Vector3(90f, cardRB.transform.rotation.eulerAngles.y, cardRB.transform.rotation.eulerAngles.z), 0.2f);
            cardInHand.transform.parent = null;
            LastCard = cardInHand;
            cardInHand.AddComponent<Card>();
            cardInHand = null;
        }
        else
        {
            Destroy(cardInHand);
            cardInHand = null;
        }
    }
}
