using UnityEngine;

[System.Serializable]
public struct Sequence 
{
    [field:SerializeField] public Beat[] beats { get; private set; }
    
    public Sequence(int pNbOfBeats, CardData pCard, bool pMustContainShoot)
    {
        beats = new Beat[pNbOfBeats];

        for (int i = 0; i < pNbOfBeats-1; i++)
        {
            beats[i] = new Beat(pCard, CardState.Declaration);
        }

        if (pMustContainShoot)
        {
            int lRndBeat = Random.Range(1, pNbOfBeats);
            beats[lRndBeat] = new Beat(pCard, CardState.Shoot);
        }
        
        beats[pNbOfBeats - 1] = new Beat(pCard, CardState.Play);
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