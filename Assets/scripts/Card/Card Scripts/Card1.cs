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
        if (cooldown > 0 && cooldown <= maxCooldown)
        {
            SetParticlesEnabled(particleGameObjects, false);
            cooldownText.text = ((int)cooldown + 1) + "";
            if (isTracking)
            {
                cooldownText.gameObject.SetActive(true);
            }
            else
            {
                cooldownText.gameObject.SetActive(false);
            }
        }
        else
        {
            cooldownText.gameObject.SetActive(false);
            if (isTracking)
            {
                SetParticlesEnabled(particleGameObjects, true);
            }
            else
            {
                SetParticlesEnabled(particleGameObjects, false);
            }
        }
    }

    public override void OnDetected()
    {
        if (cooldown <= 0 && !isTracking)
        {
            SetParticlesEnabled(attackParticleGameObjects, true);
            cooldown = maxCooldown;
            // Attack function called here
            AttackGhost atk = FindObjectOfType<AttackGhost>();
            if(atk != null)
            {
                atk.attack();
            }
        }
        isTracking = true;
    }

    public override void OnUndetected()
    {
        isTracking = false;
        SetParticlesEnabled(false);
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
