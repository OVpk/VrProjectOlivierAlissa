using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource audio1;
    [SerializeField] private AudioSource audio2;
    [SerializeField] private Enemy enemy;
    [SerializeField] private List<CardData> enemyCards;

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
    }

    private void OnDisable()
    {
        ActionManager.setTrueEnemy -= EnnemyPlayed;
        ActionManager.setTruePlayer -= PlayerPlayed;
        ActionManager.setTrueTimer -= TimerOk;
    }

    void Start()
    {
        StartRound();
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
            round[i] = new Sequence(Random.Range(2, 4), enemyCards[Random.Range(0, enemyCards.Count)]);
        }

        return round;
    }

    private IEnumerator ReadSequence()
    {
        for (int y = 0; y < round.Length; y++)
        {
            yield return new WaitForSeconds(1f);

            CardDataInstance[] sequence = round[y].beats;

            for (int i = 0; i < sequence.Length; i++)
            {
                switch (sequence[i].cardState)
                {
                    case CardState.Declaration:
                        ActionManager.playSound.Invoke(sequence[i].declarationSound);
                        yield return new WaitForSeconds(1);
                        break;
                    case CardState.Play:
                        ActionManager.playSound.Invoke(sequence[i].playSound);
                        enemy.PlaceCard(sequence[i]);
                        yield return new WaitForSeconds(1);
                        break;
                }
            }

            yield return new WaitUntil(() => haveEnemyPlayed);

            if (havePlayerPlayed && sequence[sequence.Length-1].color == playerUsedColor && haveTimerOk)
                Debug.Log("WINNNNNNNNN");
            else
                Debug.Log("LOOOOOOOSE");

            ActionManager.destroyAllCard.Invoke();

            havePlayerPlayed = false;
            haveEnemyPlayed = false;
            haveTimerOk = false;
        }
    }

    private void PlayerPlayed(CardColors color)
    {
        if (havePlayerPlayed) return;

        havePlayerPlayed = true;
        playerUsedColor = color;
    }
    private void EnnemyPlayed()
    {
        if (haveEnemyPlayed) return;

        haveEnemyPlayed = true;
    }

    private void TimerOk() => haveTimerOk = true;
}