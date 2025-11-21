using System.Collections;
using TMPro;
using UnityEngine;

public class GameIntermitant : UIFade
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Player Player;

    private int money;
    private void OnEnable()
    {

        ActionManager.endOfRound += OnRoundEnd;
        money = Player.chipNum;
        text.text = money.ToString();
    }

    private void OnDestroy()
    {
        ActionManager.endOfRound -= OnRoundEnd;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnRoundEnd()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn(group));
    }

    public void OnStopPressed()
    {
        ActionManager.AddMoney(money);
        StartCoroutine(RemoveUi(false));
        
    }
    private IEnumerator RemoveUi(bool pContinue)
    {
        yield return FadeOut(group);
        if (pContinue)
            ActionManager.startRound.Invoke();
        else
            Player.OnPlayAgain();
        gameObject.SetActive(false);
    }

    public void OnContinue()
    {
        GameManager.CurrentGameState = GameState.InUI;
        StartCoroutine(RemoveUi(true));
    }
}
