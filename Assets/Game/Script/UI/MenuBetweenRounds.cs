using System.Collections;
using TMPro;
using UnityEngine;

public class MenuBetweenRounds : UIFade
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        ActionManager.endOfRound += DisplayWindow;
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
        text.text = player.chipNum.ToString();
        StartCoroutine(FadeIn(group));
    }
    
    public void OnContinuePressed()
    {
        GameManager.CurrentGameState = GameState.InUI;
        StartCoroutine(RemoveWindow(true));
    }
    
    public void OnStopPressed()
    {
        StartCoroutine(RemoveWindow(false));
    }
    
    private IEnumerator RemoveWindow(bool wantContinue)
    {
        yield return FadeOut(group);
        if (wantContinue)
            ActionManager.startRound.Invoke(null);
        else
            ActionManager.returnToHub.Invoke();
        gameObject.SetActive(false);
    }
}
