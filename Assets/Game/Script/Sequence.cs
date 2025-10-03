using UnityEngine;

public class Sequence : MonoBehaviour
{
    private CardDataInstance[] beats;

    public Sequence(int nbOfBeats)
    {
        beats = new CardDataInstance[nbOfBeats];
    }
    
    
}
