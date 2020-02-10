using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card1 : BaseCard
{

    public ParticleSystem[] particleGameObjects;
    public ParticleSystem[] attackParticleGameObjects;
    public TextMeshPro cooldownText;
    public float maxCooldown = 3;

    [SerializeField] private GameObject debugGameObject;

    protected float cooldown = 0f;

    bool isTracking = false;

    private void Start()
    {
        Debug.Log("New particle intantiated");
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (isTracking)
        {
            if (cooldown > 0 && cooldown <= maxCooldown - 1)
            {
                cooldownText.text = ((int)cooldown + 1) + "";
                cooldownText.gameObject.SetActive(true);
                SetParticlesEnabled(particleGameObjects, false);
            }
            else
            {
                cooldownText.gameObject.SetActive(false);
                SetParticlesEnabled(particleGameObjects, false);
            }
        }
        else
        {
            SetParticlesEnabled(false);
        }
    }

    public override void OnDetected()
    {
        if(cooldown <= 0)
        {
            SetParticlesEnabled(attackParticleGameObjects ,true);
            cooldown = maxCooldown;
            //TODO: Attack function called here
        }
        isTracking = true;
    }

    public override void OnUndetected()
    {
        isTracking = false;
    }

    void SetParticlesEnabled(bool play)
    {
        if (particleGameObjects.Length != 0)
        {
            SetParticlesEnabled(particleGameObjects, play);
            SetParticlesEnabled(attackParticleGameObjects, play);
            if (debugGameObject != null)
            {
                debugGameObject.SetActive(play);
            }
        }
    }

    void SetParticlesEnabled(ParticleSystem[] particles, bool boolParam)
    {
        foreach (ParticleSystem toSetActive in particles)
        {
            toSetActive.gameObject.SetActive(boolParam);
        }
    }

}
