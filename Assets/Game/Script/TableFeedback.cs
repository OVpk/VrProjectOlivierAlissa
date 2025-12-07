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
        Bloom bloom;
        if (globalVolume.TryGet(out bloom))
        {
            spriteRenderer.material = pMat;
            DOTween.To(idk => bloom.intensity.value = idk, 0f, 12f, 0.5f);
            DOTween.To(idk => bloom.intensity.value = idk, 12f, 0f, 0.5f).OnComplete(() => spriteRenderer.material = originMat);
        }
    }
} 
