using UnityEngine;

[CreateAssetMenu(menuName = "Items")]
public class Item : MonoBehaviour
{
    [SerializeField] private Challenge unlockCondition;
    public ItemData itemData;

    public bool isUnlock = false;
    public bool bought = false;

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

    private void OnDisable()
    {
        if (unlockCondition)
        {
            unlockCondition.OnComplete -= Unlock;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Unlock()
    {
        isUnlock = true;
        unlockCondition.OnComplete -= Unlock;
    }
}