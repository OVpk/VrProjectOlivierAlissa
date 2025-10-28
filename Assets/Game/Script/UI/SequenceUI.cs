using System;
using UnityEngine;
using UnityEngine.UI;

public class SequenceUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private CardUI cardUIPrefab;

    [SerializeField] private float cardSpacing = 10f;
    [SerializeField] private float padding = 20f;

    public float Width { get; private set; }
    public float Height { get; private set; }

    public void Setup(Sequence sequence)
    {
        if (sequence == null || sequence.beats == null || sequence.beats.Length == 0)
            return;

        switch (sequence.beats[0].color)
        {
            case CardColors.Red: background.color = new Color(1f, 0.5f, 0.5f, 0.5f); break;
            case CardColors.Green: background.color = new Color(0.5f, 1f, 0.5f, 0.5f); break;
            case CardColors.Blue: background.color = new Color(0.5f, 0.5f, 1f, 0.5f); break;
        }

        RectTransform cardRectPrefab = cardUIPrefab.GetComponent<RectTransform>();
        float cardWidth = cardRectPrefab.sizeDelta.x;
        float cardHeight = cardRectPrefab.sizeDelta.y;
        int cardCount = sequence.beats.Length;

        float totalCardsWidth = cardCount * cardWidth + (cardCount - 1) * cardSpacing;
        float totalWidth = totalCardsWidth + padding * 2f;
        float totalHeight = cardHeight + padding * 2f;

        RectTransform bgRect = background.rectTransform;
        bgRect.anchorMin = new Vector2(0.5f, 0.5f);
        bgRect.anchorMax = new Vector2(0.5f, 0.5f);
        bgRect.pivot = new Vector2(0.5f, 0.5f);
        bgRect.sizeDelta = new Vector2(totalWidth, totalHeight);
        bgRect.anchoredPosition = Vector2.zero;

        RectTransform myRect = transform as RectTransform;
        if (myRect != null)
        {
            myRect.anchorMin = new Vector2(0.5f, 0.5f);
            myRect.anchorMax = new Vector2(0.5f, 0.5f);
            myRect.pivot = new Vector2(0.5f, 0.5f);
            myRect.sizeDelta = new Vector2(totalWidth, totalHeight);
        }

        Width = totalWidth;
        Height = totalHeight;

        float startX = -totalCardsWidth / 2f + (cardWidth / 2f);
        for (int i = 0; i < cardCount; i++)
        {
            var cardInstance = sequence.beats[i];
            CardUI cardUI = Instantiate(cardUIPrefab, transform);
            cardUI.Setup(cardInstance);

            RectTransform rect = cardUI.transform as RectTransform;
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localScale = Vector3.one;

            float xPos = startX + i * (cardWidth + cardSpacing);
            rect.anchoredPosition = new Vector2(xPos, 0f);
        }
    }

    private void Awake()
    {
        ActionManager.endOfRound += () => Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        ActionManager.endOfRound -= () => Destroy(this.gameObject);
    }
}
