using UnityEngine;

public class Gun : MonoBehaviour
{

    private void Awake()
    {
        ActionManager.onUiState += DisableGun;
        ActionManager.onRoundState += EnableGun;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ActionManager.onUiState -= DisableGun;
        ActionManager.onRoundState -= EnableGun;
    }

    private void EnableGun()
    {
        gameObject.SetActive(true);
    }

    private void DisableGun()
    {
        gameObject.SetActive(false);
    }

}
