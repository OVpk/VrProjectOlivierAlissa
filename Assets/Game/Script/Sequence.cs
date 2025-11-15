using UnityEngine;

public class Sequence 
{
    public CardDataInstance[] beats { get; private set; }
    
    public Sequence(int nbOfBeats, CardData card, bool mustContainShoot)
    {
        beats = new CardDataInstance[nbOfBeats];

        for (int i = 0; i < nbOfBeats-1; i++)
        {
            beats[i] = card.Instance(CardState.Declaration);
        }

        if (mustContainShoot)
        {
            int rndBeat = Random.Range(1, nbOfBeats);
            beats[rndBeat] = card.Instance(CardState.Shoot);
        }
        
        beats[nbOfBeats - 1] = card.Instance(CardState.Play);
    }
}
