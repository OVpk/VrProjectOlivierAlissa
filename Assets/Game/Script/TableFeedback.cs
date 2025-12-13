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

    private readonly float bloomValue = 12f;
    private readonly float bloomTime = .5f;

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
    private void DoEffect(Material mat)
    {
        if (globalVolume.TryGet(out Bloom bloom))
        {
            spriteRenderer.material = mat;
            DOTween.To(idk => bloom.intensity.value = idk, 0f, bloomValue, bloomTime);
            DOTween.To(idk => bloom.intensity.value = idk, bloomValue, 0f, bloomTime).OnComplete(() => spriteRenderer.material = originMat);
        }
    }
} 
