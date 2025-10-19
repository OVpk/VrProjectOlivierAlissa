using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SequenceUI : MonoBehaviour
{
    [SerializeField] GameObject elements;
    [SerializeField] GameObject color;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject check;

    private void UIPrediction(Sequence[] round)
    {
        for (int i = 0; i < round.Length; i++)
        {
            GameObject lBackGroundColor = Instantiate(color, elements.transform);
            Image lImage = lBackGroundColor.GetComponent<Image>();
            switch (round[i].beats[0].color)
            {
                case CardColors.Red:
                    lImage.color = Color.red;
                    break;
                case CardColors.Green:
                    lImage.color = Color.green;
                    break;
                case CardColors.Blue:
                    lImage.color = Color.blue;
                    break;
            } 
            for (int j = 0; j < round[i].beats.Length; j++)
            {
                switch (round[i].beats[j].cardState)
                {
                }
            }
            
        }
    }
}
