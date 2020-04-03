using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStop : MonoBehaviour
{
    public ParticleSystem[] particleGameObjects;
    public float waitForSecond = 0;

    private void Start()
    {
        Debug.Log("Started stop particle");
        StartCoroutine(SetParticleEnabledWithTimer());
    }

    IEnumerator SetParticleEnabledWithTimer()
    {
        Debug.Log("Started Coroutine");
        yield return new WaitForSeconds(waitForSecond);
        SetParticleEnabled(false);
        Debug.Log("Particle Stopped");
    }

    void SetParticleEnabled(bool play)
    {
        if (particleGameObjects.Length != 0)
        {
            foreach (ParticleSystem particle in particleGameObjects)
            {
                if (play)
                {
                    particle.Play();
                }
                else
                {
                    particle.Stop();
                }
            }
        }
    }
}
