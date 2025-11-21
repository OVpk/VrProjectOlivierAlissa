using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Challenge unlockCondition;
    public ItemData itemData;

    public bool isUnlock = false;
    public bool bought = false;
    public int id;

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

    private void OnDestroy()
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
        ActionManager.Unlock.Invoke(id);
        unlockCondition.OnComplete -= Unlock;
    }
}