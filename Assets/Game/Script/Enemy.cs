using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleGun;
    [SerializeField] private Animator animator;
    [SerializeField] private DroppedCard card;

    private readonly int triggerPlayCard = Animator.StringToHash("PlayCard");
    private readonly int triggerDeclareCard = Animator.StringToHash("DeclareCard");
    private readonly int triggerShoot = Animator.StringToHash("Shoot");
    private readonly int triggerEye = Animator.StringToHash("EyeAnim");
    private readonly int triggerMouth = Animator.StringToHash("MouthAnim");
    public bool isShooting = false;

    private void Start()
    {
        animator.SetLayerWeight(2, 1.0f);
        StartCoroutine(animMouth());
        StartCoroutine(animEye());
    }
    public void SetDisplay(CardData cardToDisplay, float timeBetweenBeats)
    {
        card.spriteDisplayer.sprite = cardToDisplay.visual;
        animator.speed = 1 / timeBetweenBeats;
    }

    public void PlaceCard() => animator.SetTrigger(triggerPlayCard);

    public void DeclareCard() => animator.SetTrigger(triggerDeclareCard);

    public void Shoot()
    {
        isShooting = true;
        animator.SetTrigger(triggerShoot);
    }

    public void PlayParticle() => particleGun.Play();

    public IEnumerator animEye()
    {
        while (true)
        {
            int randEye = Random.Range(5, 10);
            Debug.Log(randEye);
            yield return new WaitForSeconds(randEye);
            if (!isShooting)
                animator.SetTrigger(triggerEye);
        }
    }

    public IEnumerator animMouth()
    {
        while (true)
        {
            int randMouth = Random.Range(5, 10);
            yield return new WaitForSeconds(randMouth);
            animator.SetTrigger(triggerMouth);
        }
    }
}
