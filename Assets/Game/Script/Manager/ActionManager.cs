using System;
using UnityEngine;

public static class ActionManager
{
    public static Action<EnumHand> spawnCard;
    public static Action<EnumHand> removeCard;
    public static Action<EnumHand> changeCard;
    public static Action destroyAllCard;
    public static Action<CardColors> setTruePlayer;
    public static Action setTrueEnemy;
    public static Action<GameState> changeGameState;
    public static Action<AudioClip> playSound;
    public static Action startRound;
    public static Action<Sequence[]> makePredictionUI;
    public static Action resetCardInHand;
    public static Action onWin;
    public static Action onLoose;
    public static Action<Color> playParticle;
    public static Action<EnumHand> GunAppear;
    public static Action<EnumHand> GunDisapear;
    public static Action playerShoot;
    public static Action endOfRound;
    public static Action<float> enemyShoot;
    public static Action<int> numShootToGive;
    public static Action <float> timerToShoot;
}