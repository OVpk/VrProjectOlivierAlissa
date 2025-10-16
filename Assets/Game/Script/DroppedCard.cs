using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCard : MonoBehaviour
{
    public CardDataInstance cardData;
    public bool isPlayer;
    private float timer;
    private const string cardTag = "Card";
    private float timeMaxBefore = .5f;
    private void OnEnable()
    {
        ActionManager.destroyAllCard += DeleteAllCard;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(cardTag))
        {
            if (timer > timeMaxBefore)
                return;

            ActionManager.setTrueTimer.Invoke();
        }
    }

    private void OnDisable()
    {
        ActionManager.destroyAllCard -= DeleteAllCard;
    }


    private void DeleteAllCard() => Destroy(gameObject);
}
