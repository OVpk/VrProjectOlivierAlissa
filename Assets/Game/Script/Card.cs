using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isEnemy;

    private bool wonAlready;

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
