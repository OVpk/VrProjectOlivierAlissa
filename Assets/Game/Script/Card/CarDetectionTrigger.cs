using UnityEngine;

public class CarDetectionTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out DroppedCard lCard))
        {
            if (!lCard.IsDropped) return;
            if (!lCard.isPlayer) return;

            ActionManager.setTruePlayer?.Invoke(lCard.cardData.color);
        }
    }
}
