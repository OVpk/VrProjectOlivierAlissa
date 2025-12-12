using UnityEngine;

[CreateAssetMenu(fileName = "TutorialWindowData", menuName = "TutoData/TutorialWindow")]
public class TutorialWindowData : TutorialPartData
{
    [field:SerializeField] public string title { get; private set; }
    [field:SerializeField] public string description { get; private set;}
    [field:SerializeField] public GameObject imagePrefab { get; private set; }

    public override void Apply(TutoWindow pWindow)
    {
        pWindow.Setup(this);
        GameManager.CurrentGameState = GameState.InUI;
        isFinish = true;
    }
    
}
