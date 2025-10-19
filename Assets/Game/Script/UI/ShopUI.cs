using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public void OnPressed()
    {
        ActionManager.changeGameState(GameState.InRound);
        ActionManager.startRound.Invoke();
        gameObject.SetActive(false);
    }
}
