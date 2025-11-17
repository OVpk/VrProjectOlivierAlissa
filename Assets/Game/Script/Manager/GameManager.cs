using System.Collections;
using UnityEngine;

public static class GameManager
{
    private static GameState currentGameState = GameState.InRound;
    public static GameState CurrentGameState
    {
        get => currentGameState;
        set
        {
            currentGameState = value;
            if (currentGameState == GameState.InShop)
                ActionManager.resetCardInHand?.Invoke();
        }
    }
}
