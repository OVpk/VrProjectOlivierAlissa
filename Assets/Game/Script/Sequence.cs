using UnityEngine;

[System.Serializable]
public struct Sequence 
{
    [field:SerializeField] public Beat[] beats { get; private set; }
    
    public Sequence(int nbOfBeats, CardData card, bool mustContainShoot)
    {
        beats = new Beat[nbOfBeats];

        for (int i = 0; i < nbOfBeats-1; i++)
        {
            beats[i] = new Beat(card, CardState.Declaration);
        }

        if (mustContainShoot)
        {
            int rndBeat = Random.Range(1, nbOfBeats);
            beats[rndBeat] = new Beat(null, CardState.Shoot);
        }
        
        beats[nbOfBeats - 1] = new Beat(card, CardState.Play);
    }
}

[System.Serializable]
public struct Beat
{
    public CardData card;
    public CardState state;
    public Beat(CardData card, CardState state)
    {
        this.card = card;
        this.state = state;
    }
}