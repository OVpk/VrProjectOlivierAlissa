using UnityEngine;

[CreateAssetMenu(fileName = "TutorialSequenceData", menuName = "TutoData/TutorialSequence")]
public class TutorialSequenceData : TutorialPartData
{
    [field:SerializeField] public string text { get; private set; }
    [field:SerializeField] public Sequence[] customRound { get; private set; }

    private bool mustTryAgain = false;

    public override void Apply(TutoWindow window)
    {
        window.Setup(this);
        GameManager.CurrentGameState = GameState.InRound;
        StartTutoRound();

        ActionManager.endOfRound += TryRestartRound;
        ActionManager.onLoose += MustRetry;
    }
    
    private void StartTutoRound()
    {
        mustTryAgain = false;
        ActionManager.startRound.Invoke(customRound);
    }

    private void TryRestartRound()
    {
        if (mustTryAgain)
        {
            StartTutoRound();
        }
        else
        {
            ActionManager.endOfRound -= TryRestartRound;
            ActionManager.onLoose -= MustRetry;
            isFinish = true;
        }
    }

    private void MustRetry() => mustTryAgain = true;

}
