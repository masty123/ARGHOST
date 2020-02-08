using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : BaseCard
{

    public ParticleSystem particleGameObject;

    public override void OnDetected()
    {
        if(particleGameObject != null)
        {
            particleGameObject.Play();
        }
    }

    public override void OnUndetected()
    {
        if (particleGameObject != null)
        {
            particleGameObject.Stop();
        }
    }
}
