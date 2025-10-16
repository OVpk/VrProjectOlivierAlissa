using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    private GameObject card;
    private DroppedCard droppedCard;

    private void Start()
    {
        card = Instantiate(cardPrefab);
        droppedCard = card.GetComponent<DroppedCard>();
        droppedCard.IsDropped = true;
        card.SetActive(false);
    }

    public void PlaceCard(CardDataInstance cardToPlace)
    {
        ResetCardPosition();
        card.SetActive(true);

        droppedCard.spriteDisplayer.sprite = cardToPlace.visual;
    }

    private void ResetCardPosition()
    {
        card.transform.position = transform.position - (Vector3.forward * 1.5f) + Vector3.up;
        card.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
    }
}
