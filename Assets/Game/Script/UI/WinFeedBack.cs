using UnityEngine;

public class WinFeedBack : MonoBehaviour
{
    [SerializeField] private Animator starAnim;

    private void OnEnable()
    {
        ActionManager.onWin += WinFeedBackAnim;
    }

    private void OnDisable()
    {
        ActionManager.onWin -= WinFeedBackAnim;
    }

    private void WinFeedBackAnim()
    {
        starAnim.SetTrigger("winAnim");
    }

    private void FadeAnimStart()
    {
        starAnim.SetTrigger("FadeAnim");
    }
}
