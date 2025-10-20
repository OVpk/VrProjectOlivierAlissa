using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState currentGameState = GameState.InRound;
    public GameState CurrentGameState
    {
        get
        { return currentGameState; }
        private set
        {
            currentGameState = value;
            if (currentGameState == GameState.InShop)
                ActionManager.resetCardInHand.Invoke();
        }
    }

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        ActionManager.changeGameState += ChangeGameState;
    }
    private void ChangeGameState(GameState pGameState)
    {
        if (currentGameState == pGameState) return;
        currentGameState = pGameState;
    }

}
