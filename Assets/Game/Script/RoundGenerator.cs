using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundGenerator : MonoBehaviour
{
    [SerializeField] private int minNumSequence = 2;
    [SerializeField] private int maxNumSequence = 4;
    [SerializeField, Range(0,100)] private int percentOfChanceToShoot;
    
    [SerializeField] private CardData[] enemyCards;
    
    public Sequence[] GenerateRound(int pNbTotalOfBeats)
    {
        List<int> lSequencesSizes = DecomposeNumber(pNbTotalOfBeats, minNumSequence, maxNumSequence);
        Sequence[] lRound = new Sequence[lSequencesSizes.Count];
        for (int i = 0; i < lSequencesSizes.Count; i++)
        {
            lRound[i] = new Sequence(lSequencesSizes[i],
                enemyCards[Random.Range(0, enemyCards.Length)], 
                Random.Range(0,100) <= percentOfChanceToShoot);
        }
        return lRound;
    }
    
    private List<int> DecomposeNumber(int pNumber, int pMin, int pMax)
    {
        List<int> lParts = new List<int>();

        while (pNumber > 0)
        {
            if (pNumber <= pMax && pNumber >= pMin)
            {
                lParts.Add(pNumber);
                break;
            }

            int lNext = Random.Range(pMin, Mathf.Min(pMax, pNumber - pMin) + 1);
            lParts.Add(lNext);
            pNumber -= lNext;
        }
        return lParts;
    }
}
