using UnityEngine;

public class CarDetectionTrigger : MonoBehaviour
{
    // c'est un script pour détecter une voiture ?
    
    // c'est pas terrrible le trigger stay, ca le fait à chaque frame, et en plus tu combotes avec un try get component
    // niveau perf, ca sent le paté
    // je te conseille de passer par un trigger enter / exit, garder une reference vers la dropped card, et faire ce que tu as besoin avec
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
