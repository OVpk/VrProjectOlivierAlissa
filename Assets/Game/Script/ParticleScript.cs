using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void OnEnable()
    {
        ActionManager.playParticle += StartParticle;
    }

    private void OnDisable()
    {
        ActionManager.playParticle -= StartParticle;
    }
    private void StartParticle(Color pColor)
    {
        ParticleSystem.MainModule main = particle.main;
        main.startColor = pColor;
        particle.Play();
    }
}
