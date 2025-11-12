using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutoScriptable[] tutoUI = new TutoScriptable[] { };
    [SerializeField] private GameObject anchor;
    [SerializeField] private Text description;
    [SerializeField] private Text title;
    private int index = 0;
    private GameObject currentImage;

    private void Start()
    {
        OnPressed();
    }

    public void OnPressed()
    {
        if(currentImage != null) 
            Destroy(currentImage);
        currentImage = Instantiate(tutoUI[index].image, anchor.transform);
        currentImage.transform.localPosition = Vector3.zero;
        title.text = tutoUI[index].title;
        description.text = tutoUI[index].description;
        index++;
    }
}
