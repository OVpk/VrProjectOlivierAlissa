using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState { get; private set; } = GameState.InRound;

    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        ActionManager.changeGameState += ChangeGameState;
    }
    private void ChangeGameState(GameState pGameState)
    {
        if(currentGameState == pGameState) return;
        currentGameState = pGameState;
    }

}
