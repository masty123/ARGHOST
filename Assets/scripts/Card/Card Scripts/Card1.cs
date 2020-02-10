using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : BaseCard
{

    public ParticleSystem[] particleGameObjects;
    public ParticleSystem[] attackParticleGameObjects;

    [SerializeField] private GameObject debugGameObject;

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
            if (play)
            {
                SetParticlesActive(particleGameObjects, true);
                if (debugGameObject != null)
                {
                    debugGameObject.SetActive(true);
                }
            }
            else
            {
                SetParticlesActive(particleGameObjects, false);
                if (debugGameObject != null)
                {
                    debugGameObject.SetActive(false);
                }
            }
        }
    }

    void SetParticlesActive(ParticleSystem[] particles, bool boolParam)
    {
        foreach (ParticleSystem toSetActive in particles)
        {
            toSetActive.gameObject.SetActive(boolParam);
        }
    }

}
