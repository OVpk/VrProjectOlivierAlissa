using UnityEngine;

[CreateAssetMenu(menuName = "Challenges/NoHitRound Challenge")]
public class NoHitRoundChallenge : Challenge
{
    [SerializeField] private int requiredNoHitRounds;

    public override void CheckCondition<T>(T value)
    {
        if (value is int noHitRounds && noHitRounds >= requiredNoHitRounds)
            OnComplete?.Invoke();
    }
}
