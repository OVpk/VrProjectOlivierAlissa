using UnityEngine;

[CreateAssetMenu(menuName = "Challenges/NoHitRound Challenge")]
public class NoHitRoundChallenge : Challenge
{
    [SerializeField] private int requiredNoHitRounds;

    private int roundCount = 0;

    private void OnEnable()
    {
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
        CheckCondition(roundCount);
    }

    private void ResetCounter() => roundCount = 0;

    public override void CheckCondition<T>(T value)
    {
        if (value is int lNoHitRounds && lNoHitRounds >= requiredNoHitRounds)
            OnComplete?.Invoke();
    }
}
