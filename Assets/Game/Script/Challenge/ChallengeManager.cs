using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public List<Challenge> challenges;
    
    public static ChallengeManager Instance { get; private set; }
    private int roundCount = 0;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;

        ActionManager.endOfRound += IncreaseCounter;
        ActionManager.onLoose += ResetCounter;
    }

    private void OnDisable()
    {
        ActionManager.endOfRound -= IncreaseCounter;
        ActionManager.onLoose -= ResetCounter;
    }
    private void IncreaseCounter()
    {
        roundCount++;
        Notify(roundCount, typeof(NoHitRoundChallenge));
    }

    private void ResetCounter() => roundCount = 0;

    public void Notify<T>(T value, Type targetType)
    {
        foreach (Challenge challenge in challenges)
        {
            if (challenge.GetType() == targetType)
            {
                challenge.CheckCondition(value);
            }
        }
    }
}