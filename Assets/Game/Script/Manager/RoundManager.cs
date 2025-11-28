using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    [SerializeField] private int difficultyLevel = 5;
    [SerializeField] private float minusEveryRound = 0.1f;
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

    private float waitTimeSequenceUI = 3f;
    private float waitTimeBetweenSequence = 1.5f;
    private float timeBetweenNote = 1.5f;
    private float waitMargin = 0.4f;
    private float diviseur = 5f;
    private int roundCount = 0; 


    #region Init

    private void OnEnable()
    {
        ActionManager.playerShoot += PlayerShoot;
        ActionManager.startRound += InitRound;
        ActionManager.onGameOver += ResetDifficultyValue;
    }

    private void OnDisable()
    {
        ActionManager.playerShoot -= PlayerShoot;
        ActionManager.startRound -= InitRound;
        ActionManager.onGameOver -= ResetDifficultyValue;
    }

    private void Start()
    {
        ResetDifficultyValue();
    }

    private void InitRound(Sequence[] customRound = null)
    {
        ResetAllState();
        round = customRound ?? roundGenerator.GenerateRound(difficultyLevel);
        CountShootInRound();
        
        StartCoroutine(StartRound());
    }

    private void ResetDifficultyValue()
    {
        difficultyLevel = 5;
        timeBetweenNote = 1.5f;
        waitTimeSequenceUI = 3f;
        roundCount = 0;
        waitMargin = timeBetweenNote / diviseur;
    }
    
    private void CountShootInRound()
    {
        int numShot = 0;
        foreach(Sequence sequence in round)
        foreach(Beat card in sequence.beats)
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
        yield return new WaitForSeconds(waitTimeSequenceUI);
        prediction.gameObject.SetActive(false);
    }
    
    private IEnumerator ReadSequences()
    {

        for (int y = 0; y < round.Length; y++)
        {
            Beat[] sequence = round[y].beats;

            enemy.SetDisplay(sequence[0].card, timeBetweenNote);
            
            for (int i = 0; i < sequence.Length; i++)
            {
                switch (sequence[i].state)
                {
                    case CardState.Declaration :
                        yield return DeclarationBeat(sequence[i]);
                        break;
                    
                    case CardState.Shoot :
                        yield return ShootBeat();
                        break;
                    
                    case CardState.Play:
                        yield return PlayBeat(sequence[i]);
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
        yield return new WaitForSeconds(timeBetweenNote);
        ActionManager.playSound?.Invoke(beat.card.declarationSound);
    }
    
    private IEnumerator ShootBeat()
    {
        havePlayerShoot = false;
        enemy.Shoot();
        yield return new WaitForSeconds(timeBetweenNote - waitMargin);
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
        yield return new WaitForSeconds(timeBetweenNote - waitMargin);
        canPlay = true;
        Debug.Log("wait margin: " + waitMargin);
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

        Debug.Log(canPlay);
        if (!canPlay) return;
        Debug.Log(havePlayerPlayed);
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
        if (!blockLevel) return;

        int rndLevelToAdd = Random.Range(minBeatToAddForLevelUp, maxBeatToAddForLevelUp + 1);
        difficultyLevel += rndLevelToAdd;
        waitTimeSequenceUI += 0.5f;
        if (!(timeBetweenNote <= minSpeed))
            timeBetweenNote -= minusEveryRound;
        
        if (!(waitTimeBetweenSequence <= 0)) 
            waitTimeBetweenSequence -= 0.1f;
        if (!(waitMargin <= minMargin))
            waitMargin =  timeBetweenNote / diviseur;

        roundCount += 1;
        if(roundCount%5 == 0)
            ActionManager.updateMoneyLoss.Invoke();
    }
}