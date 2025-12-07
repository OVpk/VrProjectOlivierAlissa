using System;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        ActionManager.gameOver += GameOver;
    }

    private void OnDestroy()
    {
        ActionManager.gameOver -= GameOver;
    }

    private void GameOver() => DisplayScreen(true);

    private void DisplayScreen(bool state)
    {
        gameObject.SetActive(state);
        GameManager.CurrentGameState = state ? GameState.InUI : GameState.InRound;
    }

    public void ReturnToHub()
    {
        DisplayScreen(false);
        ActionManager.returnToHub.Invoke();
    }
}
