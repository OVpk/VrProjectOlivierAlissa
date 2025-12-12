using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TableFeedback : MonoBehaviour
{
    [SerializeField] private Material originMat;
    [SerializeField] private Material redMat;
    [SerializeField] private Material greenMat;
    [SerializeField] private VolumeProfile globalVolume;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float bloomValue = 12f;
    private float bloomTime = .5f;

    private void OnEnable()
    {
        ActionManager.onWin += OnWin;
        ActionManager.onLoose += OnLoose;
    }

    private void OnDisable()
    {
        ActionManager.onWin -= OnWin;
        ActionManager.onLoose -= OnLoose;
    }

    private void OnWin() => DoEffect(greenMat);
    private void OnLoose() => DoEffect(redMat);
    private void DoEffect(Material pMat)
    {
        Bloom lBloom;
        if (globalVolume.TryGet(out lBloom))
        {
            spriteRenderer.material = pMat;
            DOTween.To(idk => lBloom.intensity.value = idk, 0f, bloomValue, bloomTime);
            DOTween.To(idk => lBloom.intensity.value = idk, bloomValue, 0f, bloomTime).OnComplete(() => spriteRenderer.material = originMat);
        }
    }
} 
