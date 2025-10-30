using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedCard : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public Image spriteDisplayer;
    public CardDataInstance cardData;
    public bool isPlayer;

    private float timer;
    private float timeToFall = .3f;
    private const string cardTag = "Card";
    private float timeMaxBefore = 1F;

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
        if (!isDropped)
            return;

        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDropped)
            return;

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


    private void DeleteAllCard()
    {
        if (!isDropped)
            return;

        if(isPlayer)
            gameObject.SetActive(false);
    }
}
