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
    public static Action resetCardInHand;
    public static Action onWin;
    public static Action onLoose;
    public static Action<EnumHand> GunAppear;
    public static Action<EnumHand> GunDisapear;
    public static Action playerShoot;
    public static Action endOfRound;
    public static Action<int> numShootToGive;
}