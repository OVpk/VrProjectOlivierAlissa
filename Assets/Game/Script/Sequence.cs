using UnityEngine;

public class Sequence 
{
    public CardDataInstance[] beats { get; private set; }
    private int chanceToShoot = 0;

    public Sequence(int nbOfBeats, CardData card)
    {
        beats = new CardDataInstance[nbOfBeats];
        bool shouldShoot = false;

        if(nbOfBeats > 2)
        {
            int random = Random.Range(1, 10);
            if(random <= chanceToShoot)
            {
                shouldShoot = true;
            }
        }

        for (int i = 0; i < nbOfBeats-1; i++)
        {
            beats[i] = card.Instance(CardState.Declaration);
        }
        beats[nbOfBeats - 1] = card.Instance(CardState.Play);

        if (shouldShoot)
        {
            int shootIndex = Random.Range(1, nbOfBeats - 1);
            beats[shootIndex] = card.Instance(CardState.Shoot);
        }

    }
    
    
}
