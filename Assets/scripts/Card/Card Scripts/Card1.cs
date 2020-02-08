using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : BaseCard
{

    public ParticleSystem[] particleGameObjects;

    private void Start()
    {
        Debug.Log("New particle intantiated");
    }

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
                if (play && !particle.isPlaying)
                {
                    VDebug.Instance.Log("Particle Started");
                    particle.Play();
                }
                else if(!particle.isStopped)
                {
                    VDebug.Instance.Log("Particle stopped");
                    particle.Stop();
                }
            }
        }
        Debug.Log(particleGameObjects[0].isPlaying);
    }

}
