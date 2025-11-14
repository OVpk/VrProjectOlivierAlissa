using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Canvas tutoVisual;
    [SerializeField] private TutoScriptable[] tutoUI = new TutoScriptable[] { };
    [SerializeField] private GameObject anchor;
    [SerializeField] private Text description;
    [SerializeField] private Text title;
    [SerializeField] private Enemy enemy;
    [SerializeField] private CardData enemyCards;
    [SerializeField] private Transform anchorPart2;
    [SerializeField] private Transform anchorPart1;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonPlay;

    private int index = 0;
    private bool repeatAnim = true;
    private GameObject currentImage;

    private void Start()
    {
        OnPressed();
    }

    public void OnPressed()
    {
        if (currentImage != null)
            Destroy(currentImage);

        if(index == tutoUI.Length)
        {
            buttonNext.gameObject.SetActive(false);
            buttonPlay.gameObject.SetActive(true);
            description.text = "Tu est maintenant pret a jouer";
            title.text = "fin du tutoriel";
            tutoVisual.transform.position = anchorPart1.position;
            return;
        }
        if (tutoUI[index].image != null)
        {
            currentImage = Instantiate(tutoUI[index].image, anchor.transform);
            currentImage.transform.localPosition = Vector3.zero;
        }
        else
        {
            if(tutoVisual.transform.position != anchorPart2.position)
                tutoVisual.transform.position = anchorPart2.position;
            EnemyAnimate(tutoUI[index].cardState);
        }

        title.text = tutoUI[index].title;
        description.text = tutoUI[index].description;
        index++;
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("alissa");
    }


    private void EnemyAnimate(CardState state)
    {
        enemy.SetDisplay(enemyCards.Instance(CardState.Declaration), 3);

        switch (state)
        {
            case CardState.Declaration:
                enemy.DeclareCard();
                break;
            case CardState.Shoot:
                enemy.Shoot(); 
                break;
            case CardState.Play:
                enemy.PlaceCard();
                break;  
        }
    }
}
