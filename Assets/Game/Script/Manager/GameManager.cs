using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState currentGameState = GameState.InRound;
    public GameState CurrentGameState
    {
        get => currentGameState;
        set
        {
            currentGameState = value;
            if (currentGameState == GameState.InShop)
                ActionManager.resetCardInHand?.Invoke();
        }
    }

    public static GameManager instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

}
