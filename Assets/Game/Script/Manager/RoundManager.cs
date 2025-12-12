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

    private bool havePlayerPlayed;
    private bool havePlayerShoot;
    private Sequence[] round;
    private CardColors playerUsedColor;

    private bool canPlay = false;
    private bool canShoot = false;

    private float baseWaitTimeBetweenUi = 3f;
    private float currentWaitTimeSequenceUI;
    private float timeAddedBetweenUi = .5f;
    private float timeBetweenSequenceMinus = .1f;
    private float waitTimeBetweenSequence = 1.5f;
    private float currentTimeBetweenNote;
    private float waitMargin = 0.4f;
    private float diviseur = 5f;
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

    private void InitRound(Sequence[] pCustomRound = null)
    {
        ResetAllState();
        round = pCustomRound ?? roundGenerator.GenerateRound(currentDifficultyLevel);
        CountShootInRound();

        StartCoroutine(StartRound());
    }

    private void ResetDifficultyValue()
    {
        currentDifficultyLevel = baseDifficultyLevel;
        currentTimeBetweenNote = baseTimeBetweenNote;
        currentWaitTimeSequenceUI = baseWaitTimeBetweenUi;
        roundCount = 0;
        waitMargin = currentTimeBetweenNote / diviseur;
    }

    private void CountShootInRound()
    {
        int lNumShot = 0;
        foreach (Sequence lSequence in round)
            foreach (Beat lCard in lSequence.beats)
                if (lCard.state == CardState.Shoot) lNumShot++;

        ActionManager.numShootToGive?.Invoke(lNumShot);
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

        for (int y = 0; y < round.Length; y++)
        {
            Beat[] lSequence = round[y].beats;

            enemy.SetDisplay(lSequence[0].card, currentTimeBetweenNote);

            for (int i = 0; i < lSequence.Length; i++)
            {
                switch (lSequence[i].state)
                {
                    case CardState.Declaration:
                        yield return DeclarationBeat(lSequence[i]);
                        break;

                    case CardState.Shoot:
                        yield return ShootBeat();
                        break;

                    case CardState.Play:
                        yield return PlayBeat(lSequence[i]);
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
        yield return new WaitForSeconds(currentTimeBetweenNote - waitMargin);
        canShoot = true;

        yield return new WaitForSeconds(waitMargin);
        ActionManager.playSound(shootSong);

        yield return new WaitForSeconds(waitMargin);
        canShoot = false;
        if (havePlayerShoot)
            ActionManager.onWin.Invoke();
        else
            ActionManager.onLoose?.Invoke();
    }

    private IEnumerator PlayBeat(Beat beat)
    {
        ActionManager.setTruePlayer += PlayerPlayed;
        enemy.PlaceCard();
        yield return new WaitForSeconds(currentTimeBetweenNote - waitMargin);
        canPlay = true;
        yield return new WaitForSeconds(waitMargin);
        ActionManager.playSound(beat.card.playSound);

        yield return new WaitForSeconds(waitMargin);
        canPlay = false;

        Debug.Log(havePlayerPlayed);
        if (havePlayerPlayed && beat.card.color == playerUsedColor)
            ActionManager.onWin?.Invoke();
        else
            ActionManager.onLoose?.Invoke();
    }

    #endregion

    #region Player Actions Catcher

    private void PlayerPlayed(CardColors pColor)
    {
        ActionManager.setTruePlayer -= PlayerPlayed;

        if (!canPlay) return;
        if (havePlayerPlayed) return;

        havePlayerPlayed = true;
        playerUsedColor = pColor;
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

        int lRndLevelToAdd = Random.Range(minBeatToAddForLevelUp, maxBeatToAddForLevelUp + 1);
        currentDifficultyLevel += lRndLevelToAdd;
        currentWaitTimeSequenceUI += timeAddedBetweenUi;
        if (!(currentTimeBetweenNote <= minSpeed))
            currentTimeBetweenNote -= minusEveryRound;

        if (!(waitTimeBetweenSequence <= 0))
            waitTimeBetweenSequence -= timeBetweenSequenceMinus;
        if (!(waitMargin <= minMargin))
            waitMargin = currentTimeBetweenNote / diviseur;

        roundCount += 1;
        if (roundCount % UpMoneyRoundCount == 0)
            ActionManager.updateMoneyLoss.Invoke();
    }
}