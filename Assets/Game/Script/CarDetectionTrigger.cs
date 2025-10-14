using UnityEngine;

public class CarDetectionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Card") && other.TryGetComponent( out DroppedCard card))
        {
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
