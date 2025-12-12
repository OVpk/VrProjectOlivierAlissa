using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleGun;
    [SerializeField] private Animator animator;
    [SerializeField] private DroppedCard card;

    private const string triggerPlayCard = "PlayCard";
    private const string DeclareTrigger = "DeclareCard";
    private const string shootTrigger = "Shoot";
    public void SetDisplay(CardData pCardToDisplay, float pTimeBetweenBeats)
    {
        card.spriteDisplayer.sprite = pCardToDisplay.visual;
        animator.speed = 1/pTimeBetweenBeats;
    }

    public void PlaceCard() => animator.SetTrigger(triggerPlayCard);

    public void DeclareCard() => animator.SetTrigger(DeclareTrigger);

    public void Shoot() => animator.SetTrigger(shootTrigger);

    public void PlayParticle() => particleGun.Play();

}
