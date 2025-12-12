using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public List<Challenge> challenges;
    
    public static ChallengeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Notify<T>(T pValue, Type pTargetType)
    {
        foreach (Challenge lChallenge in challenges)
        {
            if (lChallenge.GetType() == pTargetType)
            {
                lChallenge.CheckCondition(pValue);
            }
        }
    }
}