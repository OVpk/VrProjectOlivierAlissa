using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialPartData", menuName = "TutoData/TutorialPartData")]
public abstract class TutorialPartData : ScriptableObject
{
    [field:NonSerialized] public bool isFinish { get; protected set; } = false;
    public abstract void Apply(TutoWindow window);
}
