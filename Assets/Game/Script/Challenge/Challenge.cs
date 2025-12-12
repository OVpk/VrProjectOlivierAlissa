using System;
using UnityEngine;

public abstract class Challenge : ScriptableObject
{
    public Action OnComplete;
    
    public abstract void CheckCondition<T>(T pValue);
}
