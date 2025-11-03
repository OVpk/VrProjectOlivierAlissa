using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyCardDisplay;
    private DroppedCard droppedCard;

    [SerializeField] private Animator animator;

    private void Start()
    {
        droppedCard = enemyCardDisplay.GetComponent<DroppedCard>();
        droppedCard.IsDropped = true;
    }

    public void SetDisplay(CardDataInstance cardToPlace, float timeBetweenBeats)
    {
        droppedCard.spriteDisplayer.sprite = cardToPlace.visual;
        animator.speed = 1/timeBetweenBeats;
        droppedCard.gameObject.SetActive(true);
    }

    public void PlaceCard() => animator.SetTrigger("PlayCard");

    public void DeclareCard() => animator.SetTrigger("DeclareCard");

}
