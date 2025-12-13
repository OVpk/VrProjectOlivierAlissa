using System;
using UnityEngine;

public abstract class TutorialPartData : ScriptableObject
{
    [field:NonSerialized] public bool isFinish { get; protected set; } = false;
    public abstract void Apply(TutoWindow window);
}
