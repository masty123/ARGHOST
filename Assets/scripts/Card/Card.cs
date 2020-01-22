using UnityEngine;
using Vuforia;

public abstract class Card : MonoBehaviour, ITrackableEventHandler
{

    [SerializeField] protected GameObject particlePrefab;
    [SerializeField] protected float cooldown;

    protected GameObject particleGraphics;
    protected float remainingCooldown = 0f;

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
    }

    public virtual void OnTrackingFound()
    {
        Debug.Log("Tracking");
        Debug.Log("Create particle");
        particleGraphics = Instantiate(particlePrefab, transform);
        //TODO: Attack and cooldown here? It will not attack again if the card is not lost the tracking even if the cooldown is done.
    }

    public virtual void OnTrackingLost()
    {
        Debug.Log("Untracking");
        if (particleGraphics != null)
        {
            Debug.Log("Destroy particle");
            Destroy(particleGraphics);
            //TODO: Release cooldown or do something when it is gone?
        }
    }

}
