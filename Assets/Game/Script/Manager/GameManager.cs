using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState currentGameState = GameState.InRound;
    public GameState CurrentGameState
    {
        get
        { return currentGameState; }
        set
        {
            currentGameState = value;
            if (currentGameState == GameState.InShop)
                ActionManager.resetCardInHand?.Invoke();
        }
    }

    [SerializeField] private GunPlayer GunPlayer;

    public bool canShoot = false;
    private Coroutine playerShootCoroutine;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        ActionManager.changeGameState += ChangeGameState;
        
    }

    private void OnEnable()
    {
        ActionManager.numShootToGive += SetNumShot;
        ActionManager.timerToShoot += StartPlayerShoot;
    }

    private void OnDestroy()
    {
        ActionManager.changeGameState -= ChangeGameState;
        ActionManager.numShootToGive -= SetNumShot;
        ActionManager.timerToShoot -= StartPlayerShoot;
    }
    private void ChangeGameState(GameState pGameState)
    {
        if (currentGameState == pGameState) return;
        currentGameState = pGameState;
    }

    private void SetNumShot(int pNum)
    {
        GunPlayer.NumMaxShootSequence = pNum;
    }

    private void StartPlayerShoot(float pTimeToWait)
    {
        playerShootCoroutine = StartCoroutine(TimerPlayerShoot(pTimeToWait));
    }
    private IEnumerator TimerPlayerShoot(float pTimeToWait)
    {
        yield return new WaitForSeconds(pTimeToWait);
        canShoot = true;
        yield return new WaitForSeconds(1f);
        canShoot = false;
        ActionManager.onLoose?.Invoke();
    }

    private void StopCoroutineTimer()
    {
        if (!canShoot)
            return;

        StopCoroutine(playerShootCoroutine);
    }
}
