using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject gunIcon;
    [SerializeField] private GameObject checkIcon;
    [SerializeField] private GameObject croixIcon;

    public void Setup(CardDataInstance cardToDisplay)
    {
        cardImage.sprite = cardToDisplay.visual;
        switch (cardToDisplay.cardState)
        {
            case CardState.Shoot : gunIcon.SetActive(true); break;
            case CardState.Play : checkIcon.SetActive(true); break;
            case CardState.Declaration : croixIcon.SetActive(true); break;
        }
    }
}