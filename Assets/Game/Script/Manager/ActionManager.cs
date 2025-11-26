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
    public static Action startRound;
    public static Action onUiState;
    public static Action onRoundState;
    public static Action onWin;
    public static Action onLoose;
    public static Action<EnumHand> GunAppear;
    public static Action<EnumHand> GunDisapear;
    public static Action playerShoot;
    public static Action endOfRound;
    public static Action<int> numShootToGive;
    public static Action onGameOver;
    public static Action<int> addMoney;
    public static Action<int> unlock;
    public static Action updateMoneyLoss;
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
        GunAppear=null;
        GunDisapear=null;
        playerShoot=null;
        endOfRound=null;
        numShootToGive=null;
        onGameOver=null;
        addMoney =null;
        unlock=null;
        updateMoneyLoss = null;
}
}