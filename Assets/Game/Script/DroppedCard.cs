using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCard : MonoBehaviour
{
    public CardDataInstance cardData;

    public bool isPlayer;
    private void OnEnable()
    {
        ActionManager.destroyAllCard += DeleteAllCard;
    }

    private void OnDisable()
    {
        ActionManager.destroyAllCard -= DeleteAllCard;
    }


    private void DeleteAllCard() => Destroy(gameObject);
}
