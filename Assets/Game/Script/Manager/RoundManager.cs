using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    [SerializeField] private int difficultyLevel = 5;
    [SerializeField] private float minusEveryRound = 0.1f;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private GlobalCardsUI prediction;
    [SerializeField] private RoundGenerator roundGenerator;
    [SerializeField] private int minBeatToAddForLevelUp = 1;
    [SerializeField] private int maxBeatToAddForLevelUp = 3;
    [SerializeField] private Tutorial tutorial;

    private bool haveEnemyPlayed;
    private bool havePlayerPlayed;
    private bool havePlayerShoot;
    private Sequence[] round;
    private CardColors playerUsedColor;

    private bool canPlay = false;
    private bool canShoot = false;

    private float waitTimeSequenceUI = 3f;
    private float waitMargin = .2f;
    private float waitBetweenRound = 1.5f;

    public float timeBetweenNote = 1f;
    public bool isTutorial;

    #region Init

    private void OnEnable()
    {
        ActionManager.playerShoot += PlayerShoot;
        ActionManager.startRound += InitRound;
    }

    private void OnDisable()
    {
        ActionManager.playerShoot -= PlayerShoot;
        ActionManager.startRound -= InitRound;

    }

    private void Start()
    {
        if (isTutorial)
            return;
        ResetDifficultyValue();
        InitRound();
    }

    private void InitRound()
    {
        ResetAllState();
        round = roundGenerator.GenerateRound(difficultyLevel);
        CountShootInRound();
        StartCoroutine(StartRound());
    }

    private void ResetDifficultyValue()
    {
        difficultyLevel = 5;
        timeBetweenNote = 1f;
        waitTimeSequenceUI = 3f;
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
        haveEnemyPlayed = false;
    }

    #endregion
    
    private IEnumerator StartRound()
    {
        yield return ShowUI();
        GameManager.CurrentGameState = GameState.InRound;
        StartCoroutine(ReadSequences());
    }
    
    private IEnumerator ShowUI()
    {
        GameManager.CurrentGameState = GameState.InShop;
        prediction.gameObject.SetActive(true);
        prediction.Setup(round);
        yield return new WaitForSeconds(waitTimeSequenceUI);
        prediction.gameObject.SetActive(false);
    }
    
    private IEnumerator ReadSequences()
    {
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

            yield return new WaitForSeconds(waitBetweenRound);
            ResetAllState();
        }
        DifficultyLevelUp();

        if(!isTutorial) 
            ActionManager.endOfRound.Invoke();
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
        ActionManager.playSound(beat.declarationSound);
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
                ActionManager.onLoose?.Invoke();
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
    }
}