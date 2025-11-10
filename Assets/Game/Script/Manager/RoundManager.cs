using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class RoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource audio1;
    [SerializeField] private AudioSource audio2;
    [SerializeField] private Enemy enemy;

    [SerializeField] private int difficultyLevel = 5;

    [SerializeField] private float timeBetweenNote = 1f;
    [SerializeField] private float minusEveryRound = 0.1f;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private GameObject shop;
    [SerializeField] private GlobalCardsUI prediction;
    [SerializeField] private RoundGenerator roundGenerator;
    [SerializeField] private int minBeatToAddForLevelUp = 1;
    [SerializeField] private int maxBeatToAddForLevelUp = 3;

    private bool haveEnemyPlayed;
    private bool havePlayerPlayed;
    private bool havePlayerShoot;
    private Sequence[] round;
    private CardColors playerUsedColor;

    private bool canPlay = false;
    private bool canShoot = false;

    private void OnEnable()
    {
        ActionManager.setTrueEnemy += EnnemyPlayed;
        ActionManager.playerShoot += PlayerShoot;
        ActionManager.startRound += StartRound;

        ResetAllState();
        ResetDifficultyValue();
        StartRound();
    }

    private void OnDisable()
    {
        ActionManager.setTrueEnemy -= EnnemyPlayed;
        ActionManager.playerShoot -= PlayerShoot;
        ActionManager.startRound -= StartRound;
    }

    private void ResetDifficultyValue()
    {
        //timeBetweenNote = 1f;
        //Ici on pourra rajouter toutes les valeurs lié a la difficulté a reinitialiser quand on meurt
        //comme la vitesse a taille des rounds etc...  fin ta capter quoi
    }

    private IEnumerator ShowUI()
    {
        GameManager.instance.CurrentGameState = GameState.InShop;
        prediction.gameObject.SetActive(true);
        prediction.Setup(round);
        yield return new WaitForSeconds(10f);
        StartCoroutine(ReadSequence());
        GameManager.instance.CurrentGameState = GameState.InRound;
        prediction.gameObject.SetActive(false);
    }

    private void DifficultyLevelUp()
    {
        int rndLevelToAdd = Random.Range(minBeatToAddForLevelUp, maxBeatToAddForLevelUp + 1);
        difficultyLevel += rndLevelToAdd;
        
        //timeBetweenNote -= minusEveryRound;
        //timeBetweenNote = Mathf.Clamp(timeBetweenNote, minSpeed, 1f);
    }

    private void StartRound()
    {
        round = roundGenerator.GenerateRound(difficultyLevel);
        StartCoroutine(ShowUI());
    }
    
    private void CountShootInRound()
    {
        int numShot = 0; 

        foreach(Sequence sequence in round)
        {
            foreach(CardDataInstance card in sequence.beats)
            {
                if (card.cardState == CardState.Shoot)
                    numShot++;
            }
        }
        ActionManager.numShootToGive?.Invoke(numShot);
    }



    private IEnumerator ReadSequence()
    {
        //CountShootInRound();
        for (int y = 0; y < round.Length; y++)
        {
            yield return new WaitForSeconds(timeBetweenNote);

            CardDataInstance[] sequence = round[y].beats;

            enemy.SetDisplay(sequence[0], timeBetweenNote);
            
            for (int i = 0; i < sequence.Length; i++)
            {
                switch (sequence[i].cardState)
                {
                    case CardState.Declaration :
                        enemy.DeclareCard();
                        yield return new WaitForSeconds(timeBetweenNote);
                        ActionManager.playSound(sequence[i].declarationSound);
                        break;

                    
                    case CardState.Shoot :
                        havePlayerShoot = false;
                        enemy.Shoot();
                        yield return new WaitForSeconds(timeBetweenNote - 0.2f);
                        canShoot = true;

                        yield return new WaitForSeconds(0.2f);
                        ActionManager.playSound(sequence[i].playSound);

                        yield return new WaitForSeconds(0.2f);
                        canShoot = false;

                        if (havePlayerShoot)
                        {
                            Debug.Log("WINNNNNNNNN SHOOOOT");
                        }
                        else
                        {
                            Debug.Log("LOOOOOOOSE SHOOOOT");
                            ActionManager.onLoose?.Invoke();
                        }
                        break;


                    case CardState.Play:
                        ActionManager.setTruePlayer += PlayerPlayed;
                        enemy.PlaceCard();
                        yield return new WaitForSeconds(timeBetweenNote -0.2f);
                        canPlay = true;

                        yield return new WaitForSeconds(0.2f);
                        ActionManager.playSound(sequence[i].playSound);

                        yield return new WaitUntil(() => haveEnemyPlayed);
                        yield return new WaitForSeconds(0.2f);
                        canPlay = false;
                        if (havePlayerPlayed && sequence[sequence.Length - 1].color == playerUsedColor)
                        {
                            Debug.Log("WINNNNNNNNN");
                            ActionManager.onWin?.Invoke();
                        }
                        else
                        {
                            Debug.Log("LOOOOOOOSE");
                            ActionManager.onLoose?.Invoke();
                        }


                        break;

                }
            }

            yield return new WaitForSeconds(1.5f);
            ResetAllState();


        }

        DifficultyLevelUp();

        yield return new WaitForSeconds(1);
        ActionManager.endOfRound.Invoke();

        GameManager.instance.CurrentGameState = GameState.InShop;
        shop.SetActive(true);
    }

    private void ResetAllState()
    {
        ActionManager.destroyAllCard?.Invoke();

        havePlayerPlayed = false;
        haveEnemyPlayed = false;
    }

    private void PlayerPlayed(CardColors pColor)
    {
        ActionManager.setTruePlayer -= PlayerPlayed;
        Debug.Log(canPlay);

        if (!canPlay) 
            return;
        if (havePlayerPlayed) 
            return;
        havePlayerPlayed = true;
        playerUsedColor = pColor;
    }

    private void PlayerShoot()
    {
        if (!canShoot)
            return;
        havePlayerShoot = true;
    }

    private void EnnemyPlayed()
    {
        if (haveEnemyPlayed) return;

        haveEnemyPlayed = true;
    }

}