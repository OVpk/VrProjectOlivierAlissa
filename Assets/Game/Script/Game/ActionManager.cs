using System;
using UnityEngine;

public static class ActionManager
{
    public static Action<EnumHand> spawnCard;
    public static Action<EnumHand> removeCard;
    public static Action<EnumHand> changeCard;
    public static Action destroyAllCard;
    public static Action<CardColors> setTruePlayer;
    public static Action<AudioClip> playSound;
    public static Action<Sequence[]> startRound;
    public static Action onUiState;
    public static Action onRoundState;
    public static Action onWin;
    public static Action onLoose;
    public static Action playerShoot;
    public static Action endOfRound;
    public static Action<int> numShootToGive;
    public static Action returnToHub;
    public static Action<int> unlock;
    public static Action updateMoneyLoss;
    public static Action gameOver;
    public static Action<float> beatStart;
    public static Action OnTutoLaunch;
    
    public static void Reset()
    {
        spawnCard=null;
        removeCard=null;
        changeCard=null;
        destroyAllCard=null;
        setTruePlayer=null;
        playSound=null;
        startRound=null;
        onUiState=null;
        onWin=null;
        onLoose=null;
        playerShoot=null;
        endOfRound=null;
        numShootToGive=null;
        returnToHub=null;
        unlock=null;
        updateMoneyLoss = null;
        gameOver = null;
        beatStart=null;
    }
}