using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : BaseCard
{

    public ParticleSystem[] particleGameObjects;

    public override void OnDetected()
    {
        SetParticleEnabled(true);
    }

    public override void OnUndetected()
    {
        SetParticleEnabled(false);
    }

    void SetParticleEnabled(bool play)
    {
        if (particleGameObjects.Length != 0)
        {
            foreach(ParticleSystem particle in particleGameObjects)
            {
                if (play)
                {
                    VDebug.Instance.Log("Particle Started");
                    particle.Play();
                }
                else
                {
                    VDebug.Instance.Log("Particle stopped");
                    particle.Stop();
                    Destroy(particle);
                }
            }
        }
    }

}
