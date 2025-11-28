using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public void OnPressed()
    {
        GameManager.CurrentGameState = GameState.InRound;
        ActionManager.startRound.Invoke(null);
        gameObject.SetActive(false);
    }
}
