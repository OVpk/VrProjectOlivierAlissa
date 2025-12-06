using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedCard : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public Image spriteDisplayer;
    public CardData cardData;

    public bool isPlayer;
    private float timer;
    private float timeMaxBefore = 1f;

    private bool isDropped;
    public bool IsDropped
    {
        get
        {
            return this.isDropped;
        }
        set
        {
            if (value)
                timer = 0;
            isDropped = value;
        }
    }

    private void OnEnable()
    {
        ActionManager.destroyAllCard += DeleteAllCard;
    }

    private void Start()
    {
        if (!isPlayer)
            rb.useGravity = false;
    }
    private void Update()
    {
        if (!isDropped || !isPlayer)
            return;

        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDropped || !isPlayer)
            return;

        if (timer > timeMaxBefore)
            return;
    }

    private void OnDisable()
    {
        ActionManager.destroyAllCard -= DeleteAllCard;
    }


    private void DeleteAllCard()
    {
        if (!isDropped)
            return;

        gameObject.SetActive(false);
    }
}
