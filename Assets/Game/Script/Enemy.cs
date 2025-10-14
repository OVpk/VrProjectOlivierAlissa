using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceCard()
    {
        GameObject card = Instantiate(cardPrefab);
        card.AddComponent<Card>();
        Rigidbody cardRB = card.GetComponent<Rigidbody>();
        Card check = cardRB.GetComponent<Card>();

        check.isEnemy = true;
        cardRB.isKinematic = false;
        card.transform.position = transform.position - (Vector3.forward * 1.7f) + Vector3.up;
    }
}
