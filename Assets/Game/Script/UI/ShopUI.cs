using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public void OnPressed()
    {
        GameManager.CurrentGameState = GameState.InRound;
        ActionManager.startRound.Invoke();
        gameObject.SetActive(false);
    }
}
