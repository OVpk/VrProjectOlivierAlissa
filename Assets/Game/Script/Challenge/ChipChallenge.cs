using UnityEngine;

[CreateAssetMenu(menuName = "Challenges/Chip Challenge")]
public class ChipChallenge : Challenge
{
    [SerializeField] private int requiredChips;

    public override void CheckCondition<T>(T value)
    {
        if (value is int chips && chips >= requiredChips)
            OnComplete?.Invoke();
    }
}