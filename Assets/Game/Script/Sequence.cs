using UnityEngine;

public class Sequence : MonoBehaviour
{
    public CardDataInstance[] beats { get; private set; }

    public Sequence(int nbOfBeats, CardData card)
    {
        beats = new CardDataInstance[nbOfBeats];
        for (int i = 0; i < nbOfBeats-1; i++)
        {
            beats[i] = card.Instance(CardState.Declaration);
        }
        beats[nbOfBeats] = card.Instance(CardState.Play);
    }
    
    
}
