using UnityEngine;

public enum CardColors
{
    Red,
    Green,
    Blue
}

public enum CardState
{
    Declaration,
    Play
}

public class CardData : ScriptableObject
{
    [SerializeField] public CardColors color {get; private set;}
    
    [SerializeField] public Sprite visual {get; private set;}

    [SerializeField] public AudioClip declarationSound {get; private set;}
    
    [SerializeField] public AudioClip playSound {get; private set;}

    public CardDataInstance Instance(CardState state)
    {
        return new CardDataInstance(this, state);
    }
    
}

public class CardDataInstance
{
    public CardColors color;
    public Sprite visual;
    public AudioClip declarationSound;
    public AudioClip playSound;
    public CardState cardState;
    
    public CardDataInstance(CardData data, CardState state)
    {
        color = data.color;
        visual = data.visual;
        declarationSound = data.declarationSound;
        playSound = data.playSound;
        cardState = state;
    }
}