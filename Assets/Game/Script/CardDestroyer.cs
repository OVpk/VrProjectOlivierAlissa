using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CardDestroyer : MonoBehaviour
{
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
