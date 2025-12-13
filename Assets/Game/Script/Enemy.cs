using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleGun;
    [SerializeField] private Animator animator;
    [SerializeField] private DroppedCard card;
    
    private readonly int triggerPlayCard = Animator.StringToHash("PlayCard");
    private readonly int triggerDeclareCard = Animator.StringToHash("DeclareCard");
    private readonly int triggerShoot = Animator.StringToHash("Shoot");

    public void SetDisplay(CardData cardToDisplay, float timeBetweenBeats)
    {
        card.spriteDisplayer.sprite = cardToDisplay.visual;
        animator.speed = 1/timeBetweenBeats;
    }

    public void PlaceCard() => animator.SetTrigger(triggerPlayCard);

    public void DeclareCard() => animator.SetTrigger(triggerDeclareCard);

    public void Shoot() => animator.SetTrigger(triggerShoot);

    public void PlayParticle() => particleGun.Play();

}
