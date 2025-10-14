using System.Collections.Generic;
using UnityEngine;

public class RoundGenerator : MonoBehaviour
{
    [SerializeField] private List<CardData> enemyCards;
    private int maxNbOfSequences = 5;

    public Sequence[] GenerateRound()
    {
        Sequence[] round = new Sequence[maxNbOfSequences];
        for (int i = 0; i < maxNbOfSequences; i++)
        {
            round[i] = new Sequence(Random.Range(2, 4), enemyCards[Random.Range(0, enemyCards.Count)]);
        }

        return round;
    }
}
