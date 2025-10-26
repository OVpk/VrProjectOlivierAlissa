using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource audio1;
    [SerializeField] private AudioSource audio2;
    [SerializeField] private Enemy enemy;
    [SerializeField] private List<CardData> enemyCards;
    [SerializeField] private int minNumSequence = 2;
    [SerializeField] private int maxNumSequence = 4;
    [SerializeField] private float timeBetweenNote = 1f;
    [SerializeField] private float minusEveryRound = 0.1f;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private GameObject shop;

    private int maxNbOfSequences = 5;
    private bool haveEnemyPlayed;
    private bool havePlayerPlayed;
    private bool haveTimerOk;
    private Sequence[] round;
    private CardColors playerUsedColor;

    private void OnEnable()
    {
        ActionManager.setTrueEnemy += EnnemyPlayed;
        ActionManager.setTruePlayer += PlayerPlayed;
        ActionManager.setTrueTimer += TimerOk;
        ActionManager.startRound += StartRound;

        ResetAllState();
        ResetDifficultyValue();
        StartRound();
    }

    private void OnDisable()
    {
        ActionManager.setTrueEnemy -= EnnemyPlayed;
        ActionManager.setTruePlayer -= PlayerPlayed;
        ActionManager.setTrueTimer -= TimerOk;
        ActionManager.startRound -= StartRound;
    }

    private void ResetDifficultyValue()
    {
        timeBetweenNote = 1f;
        //Ici on pourra rajouter toutes les valeurs lié a la difficulté a reinitialiser quand on meurt
        //comme la vitesse a taille des rounds etc...  fin ta capter quoi
    }

    private void StartRound()
    {
        round = GenerateRound();
        StartCoroutine(ReadSequence());
    }

    public Sequence[] GenerateRound()
    {
        Sequence[] round = new Sequence[maxNbOfSequences];
        for (int i = 0; i < maxNbOfSequences; i++)
        {
            round[i] = new Sequence(Random.Range(minNumSequence, maxNbOfSequences), enemyCards[Random.Range(0, enemyCards.Count)]);
        }

        return round;
    }

    private IEnumerator ReadSequence()
    {
        for (int y = 0; y < round.Length; y++)
        {
            yield return new WaitForSeconds(timeBetweenNote);

            CardDataInstance[] sequence = round[y].beats;

            for (int i = 0; i < sequence.Length; i++)
            {
                switch (sequence[i].cardState)
                {
                    case CardState.Declaration:
                        ActionManager.playSound.Invoke(sequence[i].declarationSound);
                        yield return new WaitForSeconds(timeBetweenNote);
                        break;
                    case CardState.Play:
                        ActionManager.playSound.Invoke(sequence[i].playSound);
                        enemy.PlaceCard(sequence[i]);
                        yield return new WaitForSeconds(timeBetweenNote);
                        break;
                }
            }

            yield return new WaitUntil(() => haveEnemyPlayed);


            if (havePlayerPlayed && sequence[sequence.Length - 1].color == playerUsedColor && haveTimerOk)
            {
                Debug.Log("WINNNNNNNNN");
                ActionManager.onWin.Invoke();
                //ActionManager.playParticle.Invoke(Color.green);
            }
            else
            {
                Debug.Log("LOOOOOOOSE");
                ActionManager.onLoose.Invoke();
                //ActionManager.playParticle.Invoke(Color.red);
            }

            yield return new WaitForSeconds(1.5f);
            ResetAllState();

        }

        timeBetweenNote -= minusEveryRound;
        timeBetweenNote = Mathf.Clamp(timeBetweenNote, minSpeed, 1f);

        yield return new WaitForSeconds(1);

        GameManager.instance.CurrentGameState = GameState.InShop;
        shop.SetActive(true);
    }

    private void ResetAllState()
    {
        ActionManager.destroyAllCard?.Invoke();

        havePlayerPlayed = false;
        haveEnemyPlayed = false;
        haveTimerOk = false;
    }

    private void PlayerPlayed(CardColors pColor)
    {
        if (havePlayerPlayed) return;

        havePlayerPlayed = true;
        playerUsedColor = pColor;
    }

    private void EnnemyPlayed()
    {
        if (haveEnemyPlayed) return;

        haveEnemyPlayed = true;
    }

    private void TimerOk() => haveTimerOk = true;
}