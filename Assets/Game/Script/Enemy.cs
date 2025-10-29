using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] private GameObject enemyCardDisplay;
    private DroppedCard droppedCard;

    private AudioClip songToPlay;

    [SerializeField] private Animator animator;

    private CardDataInstance enemyCardData;

    public void DoSong() => ActionManager.playSound(songToPlay);
    
    private void Start()
    {
        droppedCard = enemyCardDisplay.GetComponent<DroppedCard>();
        droppedCard.IsDropped = true;
    }

    public void SetDisplay(CardDataInstance cardToPlace, float timeBetweenBeats)
    {
        enemyCardData = cardToPlace;
        
        droppedCard.spriteDisplayer.sprite = enemyCardData.visual;
        animator.speed = timeBetweenBeats;
        songToPlay = enemyCardData.declarationSound;
    }

    public void PlaceCard()
    {
        songToPlay = enemyCardData.playSound;
        
        animator.SetTrigger("PlayCard");
    }

    public void DeclareCard() => animator.SetTrigger("DeclareCard");
    
}
