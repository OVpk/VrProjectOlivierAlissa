using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyCardDisplay;
    private DroppedCard droppedCard;

    private AudioClip songToPlay;

    [SerializeField] private Animator animator;

    private CardDataInstance enemyCardData;

    public void DoSong()
    {
        ActionManager.playSound(songToPlay);
        if (songToPlay == enemyCardData.playSound)
            droppedCard.IsDropped = true;
    }

    private void Start()
    {
        droppedCard = enemyCardDisplay.GetComponent<DroppedCard>();
        //droppedCard.IsDropped = true;
    }

    public void SetDisplay(CardDataInstance cardToPlace, float timeBetweenBeats)
    {
        enemyCardData = cardToPlace;

        droppedCard.spriteDisplayer.sprite = enemyCardData.visual;
        animator.speed = 2 - timeBetweenBeats;
        songToPlay = enemyCardData.declarationSound;
        droppedCard.gameObject.SetActive(true);
        droppedCard.IsDropped = false;
    }

    public void PlaceCard()
    {
        songToPlay = enemyCardData.playSound;
        animator.SetTrigger("PlayCard");
    }

    public void DeclareCard() => animator.SetTrigger("DeclareCard");

}
