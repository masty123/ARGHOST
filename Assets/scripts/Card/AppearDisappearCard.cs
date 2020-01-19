using System.Collections;
using System.Collections.Generic;
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

    public void OnTrackingFound()
    {
        Debug.Log("Tracking");
        Debug.Log("Create particle");
        tempParticle = Instantiate(particleGraphics, transform);
    }

    public void OnTrackingLost()
    {
        Debug.Log("Untracking");
        if (tempParticle != null)
        {
            Debug.Log("Destroy particle");
            Destroy(tempParticle);
        }
    }

}
