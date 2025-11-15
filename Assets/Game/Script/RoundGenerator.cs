using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundGenerator : MonoBehaviour
{
    [SerializeField] private int minNumSequence = 2;
    [SerializeField] private int maxNumSequence = 4;
    [SerializeField, Range(0,100)] private int percentOfChanceToShoot;
    
    [SerializeField] private CardData[] enemyCards;
    
    public Sequence[] GenerateRound(int nbTotalOfBeats)
    {
        List<int> sequencesSizes = DecomposeNumber(nbTotalOfBeats, minNumSequence, maxNumSequence);
        Sequence[] round = new Sequence[sequencesSizes.Count];
        for (int i = 0; i < sequencesSizes.Count; i++)
        {
            round[i] = new Sequence(sequencesSizes[i],
                enemyCards[Random.Range(0, enemyCards.Length)], 
                Random.Range(0,100) < percentOfChanceToShoot);
        }
        return round;
    }
    
    private List<int> DecomposeNumber(int number, int min, int max)
    {
        List<int> parts = new List<int>();

        while (number > 0)
        {
            if (number <= max && number >= min)
            {
                parts.Add(number);
                break;
            }

            int next = Random.Range(min, Mathf.Min(max, number - min) + 1);
            parts.Add(next);
            number -= next;
        }
        return parts;
    }
}
