using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public abstract class Card : MonoBehaviour, ITrackableEventHandler
{

    [SerializeField] protected GameObject particlePrefab;
    [SerializeField] protected GameObject attackParticlePrefab;
    [SerializeField] protected float cooldown = 3;
    [SerializeField] protected TextMeshPro cooldownText;

    protected GameObject particleGraphics;
    protected GameObject attackParticleGraphics;
    protected float remainingCooldown = 0f;
    protected bool isPutDown = true;

    private TrackableBehaviour mTrackableBehaviour;

    private void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||

            newStatus == TrackableBehaviour.Status.TRACKED)

        {

            OnTrackingFound();

        }

        else

        {

            OnTrackingLost();

        }
    }

    private void Update()
    {
        //TODO: Countdown the cooldown here.
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            cooldownText.text = (int)cooldown + "";
            cooldownText.enabled = true;
        }
        else
        {
            cooldownText.enabled = false;
            if (particleGraphics == null)
            {
                particleGraphics = Instantiate(particlePrefab, transform);
            }
        }
    }

    public virtual void OnTrackingFound()
    {
        Debug.Log("Tracking");
        Debug.Log("Create particle");
        if (cooldown <= 0 && isPutDown)
        {
            attackParticleGraphics = Instantiate(attackParticlePrefab, transform);
        }
        //particleGraphics = Instantiate(particlePrefab, transform);
        //TODO: Attack and cooldown here? It will not attack again if the card is not lost the tracking even if the cooldown is done.
        isPutDown = false;
    }

    public virtual void OnTrackingLost()
    {
        Debug.Log("Untracking");
        isPutDown = true;
        if (particleGraphics != null)
        {
            Debug.Log("Destroy particle");
            Destroy(particleGraphics);
            //TODO: Release cooldown or do something when it is gone?
            //TODO: Attack here
        }
    }

}
