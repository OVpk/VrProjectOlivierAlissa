using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float baseTimeBetweenNote = 1.5f;
    [SerializeField] private int baseDifficultyLevel = 5;
    [SerializeField] private int currentDifficultyLevel;
    [SerializeField] private float minusEveryRound = 0.2f;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float minMargin = 0.1f;
    [SerializeField] private GlobalCardsUI prediction;
    [SerializeField] private RoundGenerator roundGenerator;
    [SerializeField] private int minBeatToAddForLevelUp = 1;
    [SerializeField] private int maxBeatToAddForLevelUp = 3;
    [SerializeField] private AudioClip shootSong;
    [SerializeField] private bool blockLevel = false;
    [SerializeField] private Transform anchorGun;
    [SerializeField] private LayerMask gunZoneLayer;
    [SerializeField] private float timeToLooseWhenError = .05f;

    private bool havePlayerPlayed;
    private bool havePlayerShoot;
    private Sequence[] round;
    private CardColors playerUsedColor;

    private bool canPlay = false;
    private bool canShoot = false;
    private bool errorInRound = false;


    private float baseWaitTimeBetweenUi = 3f;
    private float currentWaitTimeSequenceUI;
    private float timeAddedBetweenUi = .5f;
    private float timeBetweenSequenceMinus = .1f;
    private float waitTimeBetweenSequence = 1.5f;
    private float currentTimeBetweenNote;
    private float errorMargin = 0.4f;
    private float errorMarginRatio = 5f;
    private int roundCount = 0;
    private float UpMoneyRoundCount = 5f;


    #region Init

    private void OnEnable()
    {
        ActionManager.playerShoot += PlayerShoot;
        ActionManager.startRound += InitRound;
        ActionManager.returnToHub += ResetDifficultyValue;
        ActionManager.gameOver += StopAllCoroutines;
    }

    private void OnDisable()
    {
        ActionManager.playerShoot -= PlayerShoot;
        ActionManager.startRound -= InitRound;
        ActionManager.returnToHub -= ResetDifficultyValue;
        ActionManager.gameOver -= StopAllCoroutines;
    }

    private void Start()
    {
        ResetDifficultyValue();
    }

    private void InitRound(Sequence[] customRound = null)
    {
        ResetAllState();
        round = customRound ?? roundGenerator.GenerateRound(currentDifficultyLevel);
        CountShootInRound();

        StartCoroutine(StartRound());
    }

    private void ResetDifficultyValue()
    {
        currentDifficultyLevel = baseDifficultyLevel;
        currentTimeBetweenNote = baseTimeBetweenNote;
        currentWaitTimeSequenceUI = baseWaitTimeBetweenUi;
        roundCount = 0;
        errorInRound = false;
        errorMargin = currentTimeBetweenNote / errorMarginRatio;
    }

    private void CountShootInRound()
    {
        int numShot = 0;
        foreach (Sequence sequence in round)
            foreach (Beat card in sequence.beats)
                if (card.state == CardState.Shoot) numShot++;

        ActionManager.numShootToGive?.Invoke(numShot);
    }

    private void ResetAllState()
    {
        ActionManager.destroyAllCard?.Invoke();

        havePlayerPlayed = false;
    }

    #endregion

    private IEnumerator StartRound()
    {
        GameManager.CurrentGameState = GameState.InRound;
        yield return ShowUI();
        StartCoroutine(ReadSequences());
    }

    private IEnumerator ShowUI()
    {
        prediction.gameObject.SetActive(true);
        prediction.Setup(round);
        yield return new WaitForSeconds(currentWaitTimeSequenceUI);
        prediction.gameObject.SetActive(false);
    }

    private IEnumerator ReadSequences()
    {
        foreach (Sequence sequence in round)
        {
            Beat[] beats = sequence.beats;

            enemy.SetDisplay(beats[0].card, currentTimeBetweenNote);

            for (int i = 0; i < beats.Length; i++)
            {
                ActionManager.beatStart.Invoke(currentTimeBetweenNote);
                switch (beats[i].state)
                {
                    case CardState.Declaration:
                        yield return DeclarationBeat(beats[i]);
                        break;

                    case CardState.Shoot:
                        yield return ShootBeat();
                        break;

                    case CardState.Play:
                        yield return PlayBeat(beats[i]);
                        break;
                }
            }

            yield return new WaitForSeconds(waitTimeBetweenSequence);
            ResetAllState();
        }
        DifficultyLevelUp();

        GameManager.CurrentGameState = GameState.InUI;
        ActionManager.endOfRound?.Invoke();
    }

    #region BeatReaders

    private IEnumerator DeclarationBeat(Beat beat)
    {
        enemy.DeclareCard();
        yield return new WaitForSeconds(currentTimeBetweenNote);
        ActionManager.playSound?.Invoke(beat.card.declarationSound);
    }

    private IEnumerator ShootBeat()
    {
        havePlayerShoot = false;
        enemy.Shoot();
        yield return new WaitForSeconds(currentTimeBetweenNote - errorMargin);
        canShoot = true;

        yield return new WaitForSeconds(errorMargin);
        ActionManager.playSound(shootSong);

        yield return new WaitForSeconds(errorMargin);
        canShoot = false;
        enemy.isShooting = false;
        RaycastHit hit;
        if (havePlayerShoot && Physics.Raycast(anchorGun.position, anchorGun.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, gunZoneLayer))
        {
            ActionManager.onWin.Invoke();
        }
        else
        {
            ActionManager.onLoose?.Invoke();
            errorInRound = true;
        }
    }

    private IEnumerator PlayBeat(Beat beat)
    {
        ActionManager.setTruePlayer += PlayerPlayed;
        enemy.PlaceCard();
        yield return new WaitForSeconds(currentTimeBetweenNote - errorMargin);
        canPlay = true;
        yield return new WaitForSeconds(errorMargin);
        ActionManager.playSound(beat.card.playSound);

        yield return new WaitForSeconds(errorMargin);
        canPlay = false;

        if (havePlayerPlayed && beat.card.color == playerUsedColor)
        {
            ActionManager.onWin?.Invoke();
        }
        else
        {
            ActionManager.onLoose?.Invoke();
            errorInRound = true;
        }
    }

    #endregion

    #region Player Actions Catcher

    private void PlayerPlayed(CardColors playedCardColor)
    {
        ActionManager.setTruePlayer -= PlayerPlayed;

        if (!canPlay) return;
        if (havePlayerPlayed) return;

        havePlayerPlayed = true;
        playerUsedColor = playedCardColor;
    }

    private void PlayerShoot()
    {
        if (!canShoot) return;
        havePlayerShoot = true;
    }

    #endregion

    private void DifficultyLevelUp()
    {
        if (blockLevel) return;

        int rndLevelToAdd = Random.Range(minBeatToAddForLevelUp, maxBeatToAddForLevelUp + 1);
        currentDifficultyLevel += rndLevelToAdd;
        currentWaitTimeSequenceUI += timeAddedBetweenUi;

        if (!(currentTimeBetweenNote <= minSpeed) && !errorInRound)
            currentTimeBetweenNote = Mathf.Lerp(0.5f, 1.5f, 1f / Mathf.Sqrt(roundCount + 1f));
        else if(currentTimeBetweenNote <= baseTimeBetweenNote - timeToLooseWhenError)
        {
            errorInRound = false;
            currentTimeBetweenNote -= timeToLooseWhenError;
        }
        if (!(waitTimeBetweenSequence <= 0))
            waitTimeBetweenSequence -= timeBetweenSequenceMinus;
        if (!(errorMargin <= minMargin))
            errorMargin = currentTimeBetweenNote / errorMarginRatio;

        roundCount += 1;
        if (roundCount % UpMoneyRoundCount == 0)
            ActionManager.updateMoneyLoss.Invoke();
    }
}