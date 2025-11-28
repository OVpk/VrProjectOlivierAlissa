using TMPro;
using UnityEngine;

public class TutoSequence : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogue;

    private void Setup(TutorialSequenceData data)
    {
        dialogue.text = data.text;
        
    }
}
