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

        roundManager.enabled = true;
        ActionManager.startRound.Invoke(null);
        shop.gameObject.SetActive(false);
        playGroup.gameObject.SetActive(false);
    }


    private void MenuAppear()
    {
        playButton.enabled = true;
        shop.gameObject.SetActive(true);
        gameObject.SetActive(true);

        StartCoroutine(FadeIn(shop.gameObject));
        StartCoroutine(FadeIn(playGroup.gameObject));
    }

    public void OnTutoPressed()
    {
        SceneManager.LoadScene(tutorialSceneName);
    }
}
