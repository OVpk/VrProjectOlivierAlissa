using System.Collections;
using UnityEngine;

public abstract class UIFade : MonoBehaviour
{
    private float minusFactor = 0.05f;
    protected IEnumerator FadeOut(CanvasGroup pGroup)
    {
        while (pGroup.alpha != 0)
        {
            pGroup.alpha -= minusFactor;
            yield return null;
        }
    }

    protected IEnumerator FadeIn(CanvasGroup pGroup)
    {
        while (pGroup.alpha != 1)
        {
            pGroup.alpha += minusFactor;
            yield return null;
        }
    }
}
