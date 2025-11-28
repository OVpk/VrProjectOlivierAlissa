using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleGun;
    [SerializeField] private Animator animator;
    [SerializeField] private DroppedCard card;

    public void SetDisplay(CardData cardToDisplay, float timeBetweenBeats)
    {
        card.spriteDisplayer.sprite = cardToDisplay.visual;
        animator.speed = 1/timeBetweenBeats;
    }

    public void PlaceCard() => animator.SetTrigger("PlayCard");

    public void DeclareCard() => animator.SetTrigger("DeclareCard");

    public void Shoot() => animator.SetTrigger("Shoot");

    private void PlayParticle() => particleGun.Play();

}
