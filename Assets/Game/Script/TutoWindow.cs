using UnityEngine;
using UnityEngine.UI;

public class TutoWindow : MonoBehaviour
{
    [SerializeField] private GameObject explanatoryWindow;
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private GameObject imageAnchor;
    
    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private Text dialogue;

    public void Setup(TutorialWindowData data)
    {
        explanatoryWindow.SetActive(true);
        dialogueWindow.SetActive(false);
        
        foreach (Transform child in imageAnchor.transform) 
            Destroy(child.gameObject);
        
        title.text = data.title;
        description.text = data.description;
        if (data.imagePrefab != null)
            Instantiate(data.imagePrefab, imageAnchor.transform);
    }

    public void Setup(TutorialSequenceData data)
    {
        explanatoryWindow.SetActive(false);
        dialogueWindow.SetActive(true);

        dialogue.text = data.text;
    }
}
