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
        Debug.Log("check condition");
        CheckCondition(roundCount);
    }

    private void ResetCounter() => roundCount = 0;

    public override void CheckCondition<T>(T value)
    {
        Debug.Log("value is no hit round, should be true " + value is int);
        if (value is int noHitRounds && noHitRounds >= requiredNoHitRounds)
        {
            OnComplete?.Invoke();
            Debug.Log("devrait se de bloquer");
        }
    }
}
