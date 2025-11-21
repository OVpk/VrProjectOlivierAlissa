using UnityEngine;

[CreateAssetMenu(menuName = "Items")]
public class Item : MonoBehaviour
{
    [SerializeField] private Challenge unlockCondition;

    private bool isUnlock = false;

    private void OnEnable()
    {
        if (unlockCondition)
        {
            unlockCondition.OnComplete += Unlock;
        }
        else
        {
            isUnlock = true;
        }
    }

    private void Unlock()
    {
        isUnlock = true;
        unlockCondition.OnComplete -= Unlock;
    }
}