using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialPartData[] tuto;

    [SerializeField] private TutoWindow tutoWindow;

    private bool canContinue = false;
    private const string GameScene = "Game";

    private void Start()
    {
        StartCoroutine(ReadTutorial());
    }

    private IEnumerator ReadTutorial()
    {
        foreach (TutorialPartData lTutoPart in tuto)
        {
            lTutoPart.Apply(tutoWindow);
            yield return new WaitUntil(() => lTutoPart.isFinish);

            if (lTutoPart is TutorialWindowData)
            {
                canContinue = false;
                yield return new WaitUntil(() => canContinue);
            }
        }
        SceneManager.LoadScene(GameScene);
    }

    public void ContinueButtonPressed() => canContinue = true;
}
