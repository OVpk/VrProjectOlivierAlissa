using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    public void PlaceCard(CardDataInstance cardToPlace)
    {
        GameObject card = Instantiate(cardPrefab);
        card.AddComponent<DroppedCard>();
        card.GetComponent<Card>().spriteDisplayer.sprite = cardToPlace.visual;
        Rigidbody cardRB = card.GetComponent<Rigidbody>();
        DroppedCard check = cardRB.GetComponent<DroppedCard>();
        cardRB.isKinematic = false;
        card.transform.position = transform.position - (Vector3.forward * 1.5f) + Vector3.up;
    }
}
