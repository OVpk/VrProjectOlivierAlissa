using UnityEngine;

public class CarDetectionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Card"))
        {
            Card card = other.GetComponent<Card>();

            switch (card.isPlayer)
            {
                case true:
                    ActionManager.setTruePlayer.Invoke();
                    break;
                case false:
                    ActionManager.setTrueEnemy.Invoke();
                    break;
            } 
        }
    }
}
