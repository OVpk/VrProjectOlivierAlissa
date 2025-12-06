using System;
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
    Play,
    Shoot
}

[CreateAssetMenu (fileName = "CardData" , menuName = "ScriptableObject/Card")]
public class CardData : ScriptableObject
{
    [field : SerializeField] public CardColors color {get; private set;}

    [field: SerializeField] public Sprite visual {get; private set;}

    [field: SerializeField] public AudioClip declarationSound {get; private set;}

    [field: SerializeField] public AudioClip playSound {get; private set;}

    
}