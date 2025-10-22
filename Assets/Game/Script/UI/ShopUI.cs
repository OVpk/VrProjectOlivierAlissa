using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public void OnPressed()
    {
        GameManager.instance.CurrentGameState = GameState.InRound;
        ActionManager.startRound.Invoke();
        gameObject.SetActive(false);
    }
}
