using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private int[] sheet = { 1, 1, 2 };

    [SerializeField] private AudioSource audio1;
    [SerializeField] private AudioSource audio2;
    [SerializeField] private Enemy enemy;

    private bool haveEnemyPlayed;
    private bool havePlayerPlayed;

    void Start()
    {
        StartCoroutine(ReadSequence());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ReadSequence()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < sheet.Length; i++)
        {
            switch (sheet[i])
            {
                case 1:
                    audio1.Play();
                    yield return new WaitForSeconds(1);
                    break;
                case 2:
                    audio2.Play();
                    enemy.PlaceCard();
                    yield return new WaitForSeconds(1);
                    break;
            }
        }

        yield return new WaitUntil(()=> haveEnemyPlayed);
        havePlayerPlayed == true ? Debug.Log("WIN") : Debug.Log("LOOSE");
        ActionManager.destroyAllCard.Invoke();
        StartCoroutine(ReadSequence());
    }

    private void PlayerPlayed() => havePlayerPlayed = true;
    private void EnnemyPlayed() => haveEnemyPlayed = true;
}