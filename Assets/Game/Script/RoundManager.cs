using System.Collections;
using System.Collections.Generic;
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
    private Sequence[] round;

    private void OnEnable()
    {
        ActionManager.setTrueEnemy += EnnemyPlayed;
        ActionManager.setTruePlayer += PlayerPlayed;
    }

    private void OnDisable()
    {
        ActionManager.setTrueEnemy -= EnnemyPlayed;
        ActionManager.setTruePlayer -= PlayerPlayed;
    }

    void Start()
    {
        StartRound();
        StartCoroutine(ReadSequence());
    }

    private void StartRound()
    {
        round = GenerateRound();
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
                        audio1.Play();
                        yield return new WaitForSeconds(1);
                        break;
                    case CardState.Play:
                        audio2.Play();
                        enemy.PlaceCard();
                        yield return new WaitForSeconds(1);
                        break;
                }
            }

            yield return new WaitUntil(() => haveEnemyPlayed);
            if (havePlayerPlayed)
                Debug.Log("WINNNNNNNNN");
            else
                Debug.Log("LOOOOOOOSE");

            ActionManager.destroyAllCard.Invoke();

            havePlayerPlayed = false;
            haveEnemyPlayed = false;
        }
    }

    private void PlayerPlayed() => havePlayerPlayed = true;
    private void EnnemyPlayed() => haveEnemyPlayed = true;
}