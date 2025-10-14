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
        card.AddComponent<CardDestroyer>();
        Rigidbody cardRB = card.GetComponent<Rigidbody>();
        CardDestroyer check = cardRB.GetComponent<CardDestroyer>();
        cardRB.isKinematic = false;
        card.transform.position = transform.position - (Vector3.forward * 1.5f) + Vector3.up;
    }
}
