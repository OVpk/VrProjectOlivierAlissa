using UnityEngine;

public class CarDetectionTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out DroppedCard card))
        {
            if (!card.IsDropped) return;
            if (!card.isPlayer) return;

            ActionManager.setTruePlayer?.Invoke(card.cardData.color);
        }
    }
}
