using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenue : UIFade
{
    [SerializeField] private CanvasGroup shop;
    [SerializeField] private CanvasGroup playGroup;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private Button playButton;

    private string tutorialSceneName = "Tuto";

    private void OnEnable()
    {
        GameManager.CurrentGameState = GameState.InUI;
        ActionManager.onGameOver += MenuAppear;
    }

    private void OnDestroy()
    {
        ActionManager.onGameOver -= MenuAppear;
    }
    public void OnPlayPressed()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        playButton.enabled = false;
        StartCoroutine(FadeOut(shop));
        yield return FadeOut(playGroup);
        roundManager.enabled = true;
        ActionManager.startRound.Invoke(null);
        shop.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void MenuAppear()
    {
        playButton.enabled = true;
        shop.gameObject.SetActive(true);
        gameObject.SetActive(true);
        StartCoroutine(FadeIn(shop));
        StartCoroutine(FadeIn(playGroup));
    }

    public void OnTutoPressed()
    {
        SceneManager.LoadScene(tutorialSceneName);
    }
}
