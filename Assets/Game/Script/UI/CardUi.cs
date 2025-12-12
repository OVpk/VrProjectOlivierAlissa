using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject gunIcon;
    [SerializeField] private GameObject checkIcon;
    [SerializeField] private GameObject croixIcon;

    public void Setup(Beat pBeatToDisplay)
    {
        cardImage.sprite = pBeatToDisplay.card.visual;
        switch (pBeatToDisplay.state)
        {
            case CardState.Shoot : gunIcon.SetActive(true); break;
            case CardState.Play : checkIcon.SetActive(true); break;
            case CardState.Declaration : croixIcon.SetActive(true); break;
        }
    }
}