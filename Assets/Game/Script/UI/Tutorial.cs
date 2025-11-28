using System;
using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialPartData[] tuto;

    [SerializeField] private TutoWindow tutoWindow;

    private bool canContinue = false;

    private void Start()
    {
        ActionManager.onWin += CanContinue;
    }

    private IEnumerator ReadTutorial()
    {
        foreach (TutorialPartData tutoPart in tuto)
        {
            canContinue = false;
            tutoPart.Apply(tutoWindow);
            yield return new WaitUntil(() => canContinue);
        }
    }

    public void CanContinue() => canContinue = true;
}
