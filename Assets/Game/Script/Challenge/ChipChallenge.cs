using UnityEngine;

[CreateAssetMenu(menuName = "Challenges/Chip Challenge")]
public class ChipChallenge : Challenge
{
    [SerializeField] private int requiredChips;

    public override void CheckCondition<T>(T value)
    {
        Debug.Log(value + " " + requiredChips);
        if (value is int chips && chips >= requiredChips)
        {
            Debug.Log("should unlock");
            OnComplete?.Invoke();
        }
    }
}