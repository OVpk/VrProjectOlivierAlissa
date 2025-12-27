using UnityEngine;

public class Sablier : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnEnable()
    {
        ActionManager.beatStart += PlayAnim;
    }

    private void PlayAnim(float pDuration)
    {
        animator.speed = 2.5f / pDuration;
        animator.SetTrigger("shouldPlay");
    }
}
