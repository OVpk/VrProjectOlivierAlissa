using UnityEngine;

public class CarDetectionTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out DroppedCard card))
        {
            if (!card.IsDropped) return;

            switch (card.isPlayer)
            {
                case true:
                    ActionManager.setTruePlayer.Invoke(card.cardData.color);
                    break;
                case false:
                    ActionManager.setTrueEnemy.Invoke();
                    break;
            }
        }
    }
}
