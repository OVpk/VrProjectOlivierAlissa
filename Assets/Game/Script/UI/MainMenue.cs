using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    [SerializeField] private CanvasGroup shop;
    [SerializeField] private CanvasGroup playGroup;

    private string tutorialSceneName = "tuto";
    private float minusFactor = 0.01f;

    private void OnEnable()
    {
        ActionManager.onGameOver += MenuAppear;
    }

    private void OnDestroy()
    {
        ActionManager.onGameOver -= MenuAppear;
    }
    public void OnPlayPressed()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while(playGroup.alpha != 0)
        {
            playGroup.alpha -= minusFactor;
            shop.alpha -= minusFactor;
            yield return null;
        }
        shop.gameObject.SetActive(false);
        gameObject.SetActive(false);
        ActionManager.startRound.Invoke();
    }

    private void MenuAppear()
    {
        shop.gameObject.SetActive(true);
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        while (playGroup.alpha != 1)
        {
            playGroup.alpha += minusFactor;
            shop.alpha += minusFactor;
            yield return null;
        }
    }

    public void OnTutoPressed()
    {
        SceneManager.LoadScene(tutorialSceneName);
    }
}
