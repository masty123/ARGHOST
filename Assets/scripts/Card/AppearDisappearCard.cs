using UnityEngine;
using Vuforia;

public class AppearDisappearCard : MonoBehaviour, ITrackableEventHandler
{

    [SerializeField] GameObject particleGraphics;
    GameObject tempParticle;

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

    public void OnTrackingFound()
    {
        Debug.Log("Tracking");
        Debug.Log("Create particle");
        tempParticle = Instantiate(particleGraphics, transform);
        //TODO: Attack and cooldown here? It will not attack again if the card is not lost the tracking even if the cooldown is done.
    }

    public void OnTrackingLost()
    {
        Debug.Log("Untracking");
        if (tempParticle != null)
        {
            Debug.Log("Destroy particle");
            Destroy(tempParticle);
            //TODO: Release cooldown or do something when it is gone?
        }
    }

}
