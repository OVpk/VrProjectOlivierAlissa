using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private Slider lifeSlider;
    [SerializeField] private float lifeLostByError = 0.2f;
    [SerializeField] private float lifeWin = 0.3f;
    [SerializeField] private bool godMod;

    private void OnEnable()
    {
        ActionManager.onWin += WinLife;
        ActionManager.onLoose += LooseLife;
    }

    private void OnDisable()
    {
        ActionManager.onWin -= WinLife;
        ActionManager.onLoose -= LooseLife;
    }

    private void LooseLife()
    {
        if(godMod) 
            return;
        lifeSlider.value -= lifeLostByError;
        lifeSlider.value = Mathf.Clamp01(lifeSlider.value);
    }

    private void WinLife()
    {
        if(godMod) 
            return;
        lifeSlider.value += lifeWin;
        lifeSlider.value = Mathf.Clamp01(lifeSlider.value);
    }
}
