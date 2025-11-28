using UnityEngine;

[CreateAssetMenu(fileName = "TutorialSequenceData", menuName = "TutoData/TutorialSequence")]
public class TutorialSequenceData : TutorialPartData
{
    [field:SerializeField] public string text { get; private set; }
    [field:SerializeField] public Sequence[] customRound { get; private set; }

    public override void Apply(TutoWindow window)
    {
        window.Setup(this);
        GameManager.CurrentGameState = GameState.InRound;
        StartTutoRound();
        ActionManager.endOfRound += StartTutoRound;
        ActionManager.onWin += StopTutoRound;
    }

    private void StartTutoRound() => ActionManager.startRound.Invoke(customRound);

    private void StopTutoRound(){
        ActionManager.endOfRound -= StartTutoRound;
        ActionManager.onWin -= StopTutoRound;
    }
}
