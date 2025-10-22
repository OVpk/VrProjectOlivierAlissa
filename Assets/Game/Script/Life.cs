using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private Slider lifeSlider;
    [SerializeField] private float lifeLostByError = 0.2f;
    [SerializeField] private float lifeWin = 0.3f;
    [SerializeField] private bool godMod;
    [SerializeField] private Canvas looseCanvas;
    [SerializeField] private RoundManager roundManager;

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

        if(lifeSlider.value <= 0)
        {
            looseCanvas.gameObject.SetActive(true);
            GameManager.instance.CurrentGameState = GameState.InShop;
            roundManager.StopAllCoroutines();
            roundManager.enabled = false;
        }
    }

    public void OnPlayAgain()
    {
        lifeSlider.value = 1;
        looseCanvas.gameObject.SetActive(false);
        GameManager.instance.CurrentGameState = GameState.InRound;
        roundManager.enabled = true;
    }

    private void WinLife()
    {
        if(godMod) 
            return;
        lifeSlider.value += lifeWin;
        lifeSlider.value = Mathf.Clamp01(lifeSlider.value);
    }
}
