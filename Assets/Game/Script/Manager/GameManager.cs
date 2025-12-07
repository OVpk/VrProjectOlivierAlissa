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
            if (currentGameState == GameState.InUI)
                ActionManager.onUiState?.Invoke();
            else if(currentGameState == GameState.InRound)
                ActionManager.onRoundState?.Invoke();
        }
    }
    
}
