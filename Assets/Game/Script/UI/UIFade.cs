using System.Collections;
using DG.Tweening;
using UnityEngine;
using TweenSequence = DG.Tweening.Sequence;

public abstract class UIFade : MonoBehaviour
{
    private float minusFactor = 0.05f;
    protected IEnumerator FadeOut(GameObject pGroup)
    {
        TweenSequence seq = DOTween.Sequence();
        seq.Append(pGroup.transform.DOScale(0.0f, 0.7f).SetEase(Ease.InBack));
        yield return seq.WaitForCompletion();
    }

    protected IEnumerator FadeIn(GameObject pGroup)
    {
        yield return null;
        pGroup.transform.DOScale(0.0015f, 0.7f).SetEase(Ease.OutBack);
    }
}
