using System.Collections;
using TMPro;
using UnityEngine;

public class MenuBetweenRounds : UIFade
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Player Player;

    private int money;
    private void OnEnable()
    {
        ActionManager.endOfRound += DisplayWindow;
        money = Player.chipNum;
        text.text = money.ToString();
    }

    private void OnDestroy()
    {
        ActionManager.endOfRound -= DisplayWindow;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void DisplayWindow()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn(group));
    }
    
    public void OnContinue()
    {
        GameManager.CurrentGameState = GameState.InUI;
        StartCoroutine(RemoveWindow(true));
    }
    
    public void OnStopPressed()
    {
        ActionManager.addMoney(money);
        StartCoroutine(RemoveWindow(false));
    }
    private IEnumerator RemoveWindow(bool pContinue)
    {
        yield return FadeOut(group);
        if (pContinue)
            ActionManager.startRound.Invoke(null);
        else
            Player.OnPlayAgain();
        gameObject.SetActive(false);
    }

}
