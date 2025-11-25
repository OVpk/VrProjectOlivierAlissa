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
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private GameIntermitant intermitant;

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
    public bool isTutorial;
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
        if (isTutorial)
            return;
        ResetDifficultyValue();
    }

    private void InitRound()
    {
        ResetAllState();
        round = roundGenerator.GenerateRound(difficultyLevel);
        CountShootInRound();
        if (GameManager.CurrentGameState != GameState.InRound)
            GameManager.CurrentGameState = GameState.InRound;
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
        foreach(CardDataInstance card in sequence.beats)
            if (card.cardState == CardState.Shoot) numShot++;
        
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
        roundCount += 1;

        for (int y = 0; y < round.Length; y++)
        {
            CardDataInstance[] sequence = round[y].beats;

            enemy.SetDisplay(sequence[0], timeBetweenNote);
            
            for (int i = 0; i < sequence.Length; i++)
            {
                switch (sequence[i].cardState)
                {
                    case CardState.Declaration :
                        yield return DeclarationBeat(sequence[i]);
                        break;
                    
                    case CardState.Shoot :
                        yield return ShootBeat(sequence[i]);
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

        if (!isTutorial)
        {
            GameManager.CurrentGameState = GameState.InUI;
            ActionManager.endOfRound.Invoke();
        }
        else
            tutorial.OnTutorialEnd();
    }

    #region BeatReaders

    private IEnumerator DeclarationBeat(CardDataInstance beat)
    { 
        if(tutorial)
            tutorial.OnDeclaration();
        enemy.DeclareCard();
        yield return new WaitForSeconds(timeBetweenNote);
        ActionManager.playSound?.Invoke(beat.declarationSound);
    }
    
    private IEnumerator ShootBeat(CardDataInstance beat)
    {
        if(tutorial)
            tutorial.OnShoot();
        havePlayerShoot = false;
        enemy.Shoot();
        yield return new WaitForSeconds(timeBetweenNote - waitMargin);
        canShoot = true;

        yield return new WaitForSeconds(waitMargin);
        ActionManager.playSound(beat.playSound);

        yield return new WaitForSeconds(waitMargin);
        if (!isTutorial)
        {
            canShoot = false;

            if (!havePlayerShoot)
                ActionManager.onLoose.Invoke();
        }
        else
        {
            yield return new WaitUntil(() => havePlayerShoot);
        }
    }

    private IEnumerator PlayBeat(CardDataInstance beat)
    {
        if(tutorial)
            tutorial.OnPlay();
        ActionManager.setTruePlayer += PlayerPlayed;
        enemy.PlaceCard();
        yield return new WaitForSeconds(timeBetweenNote - waitMargin);
        canPlay = true;

        yield return new WaitForSeconds(waitMargin);
        ActionManager.playSound(beat.playSound);

        if (!isTutorial)
        {
            yield return new WaitForSeconds(waitMargin);
            canPlay = false;

            if (havePlayerPlayed && beat.color == playerUsedColor)
                ActionManager.onWin?.Invoke();
            else
                ActionManager.onLoose?.Invoke();
        }
        else
        {
            yield return new WaitUntil(() => havePlayerPlayed);
            ActionManager.onWin?.Invoke();
        }
    }

    #endregion
    
    #region Player Actions Catcher

    private void PlayerPlayed(CardColors pColor)
    {
        ActionManager.setTruePlayer -= PlayerPlayed;

        if (!canPlay && !tutorial) return;
        if (havePlayerPlayed && !tutorial) return;
        
        havePlayerPlayed = true;
        playerUsedColor = pColor;
    }

    private void PlayerShoot()
    {
        if (!canShoot && !tutorial) return;
        havePlayerShoot = true;
    }

    #endregion

    private void DifficultyLevelUp()
    {
        int rndLevelToAdd = Random.Range(minBeatToAddForLevelUp, maxBeatToAddForLevelUp + 1);
        difficultyLevel += rndLevelToAdd;
        waitTimeSequenceUI += 0.5f;
        if (!(timeBetweenNote <= minSpeed))
            timeBetweenNote -= minusEveryRound;
        
        if (!(waitTimeBetweenSequence <= 0)) 
            waitTimeBetweenSequence -= 0.1f;
        if (!(waitMargin <= minMargin))
            waitMargin =  timeBetweenNote / diviseur;
    }
}