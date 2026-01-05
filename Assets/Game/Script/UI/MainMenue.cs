using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenue : UIFade
{
    [SerializeField] private CanvasGroup shop;
    [SerializeField] private CanvasGroup playGroup;
    [SerializeField] private CanvasGroup settingsGroup;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private Button playButton;

    private string tutorialSceneName = "Tuto";

    private void OnEnable()
    {
        GameManager.CurrentGameState = GameState.InUI;
        ActionManager.returnToHub += MenuAppear;
    }

    private void OnDestroy()
    {
        ActionManager.returnToHub -= MenuAppear;
    }
    public void OnPlayPressed()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        playButton.enabled = false;

        yield return StartCoroutine(FadeOut(shop.gameObject));
        yield return StartCoroutine(FadeOut(playGroup.gameObject));
        yield return StartCoroutine(FadeOut(settingsGroup.gameObject));

        roundManager.enabled = true;
        ActionManager.startRound.Invoke(null);

        shop.gameObject.SetActive(false);
        playGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
    }


    private void MenuAppear()
    {
        playButton.enabled = true;
        shop.gameObject.SetActive(true);
        settingsGroup.gameObject.SetActive(true);
        gameObject.SetActive(true);

        StartCoroutine(FadeIn(shop.gameObject));
        StartCoroutine(FadeIn(playGroup.gameObject));
        StartCoroutine(FadeIn(settingsGroup.gameObject));
    }

    public void OnTutoPressed()
    {
        ActionManager.Reset();
        SceneManager.LoadScene(tutorialSceneName);
    }
}
