using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class UIFade : MonoBehaviour
{
    private float minusFactor = 0.05f;
    protected IEnumerator FadeOut(CanvasGroup pGroup)
    {
        //while (pGroup.alpha != 0)
        //{
        //    pGroup.alpha -= minusFactor;
        //    yield return null;
        //}
        yield return null;
        pGroup.transform.DOScale(0.0f, 0.7f).SetEase(Ease.InBack).OnComplete(() => pGroup.gameObject.SetActive(false));
        //pGroup.transform.DOScale(0f, 0.5f).SetEase(Ease.InQuad).OnComplete(() => pGroup.gameObject.SetActive(false));
    }

    protected IEnumerator FadeIn(CanvasGroup pGroup)
    {
        //while (pGroup.alpha != 1)
        //{
        //    pGroup.alpha += minusFactor;
        //    yield return null;
        //}
        yield return null;
        pGroup.transform.DOScale(0.0015f, 0.7f).SetEase(Ease.OutBack);
       // pGroup.transform.DOScale(0.0015f, 0.25f).SetEase(Ease.InQuad);
    }
}
