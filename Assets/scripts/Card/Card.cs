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
        if(remainingCooldown > 0)
        {
            remainingCooldown -= Time.deltaTime;
            cooldownText.text = (int)remainingCooldown + "";
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
        if (remainingCooldown <= 0 && isPutDown)
        {
            attackParticleGraphics = Instantiate(attackParticlePrefab, transform);
            remainingCooldown = cooldown;
            //TODO: Attack here
        }
        if (particleGraphics != null)
        {
            particleGraphics.SetActive(true);
        }
        //particleGraphics = Instantiate(particlePrefab, transform);
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
        }
        if (particleGraphics != null)
        {
            particleGraphics.SetActive(false);
        }
    }

}
