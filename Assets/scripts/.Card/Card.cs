using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using Vuforia;

public abstract class Card : MonoBehaviour //s,ITrackableEventHandler
{

    //[SerializeField] protected GameObject particlePrefab;
    //[SerializeField] protected GameObject attackParticlePrefab;
    //[SerializeField] protected float cooldown = 3;
    //[SerializeField] protected TextMeshPro cooldownText;

    //protected GameObject particleGraphics;
    //protected GameObject attackParticleGraphics;
    //protected float remainingCooldown = 0f;
    //protected bool isPutDown = true;
    //protected bool isTracking = false;

    //private TrackableBehaviour mTrackableBehaviour;

    //private void Start()
    //{
    //    mTrackableBehaviour = GetComponent<TrackableBehaviour>();
    //    if (mTrackableBehaviour)
    //    {
    //        mTrackableBehaviour.RegisterTrackableEventHandler(this);
    //    }
    //}

    //public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    //{
    //    if (newStatus == TrackableBehaviour.Status.DETECTED ||

    //        newStatus == TrackableBehaviour.Status.TRACKED)

    //    {

    //        OnTrackingFound();

    //    }

    //    else

    //    {

    //        OnTrackingLost();

    //    }
    //}

    //private void Update()
    //{
    //    //TODO: Countdown the cooldown here.
    //    remainingCooldown -= Time.deltaTime;
    //    if (remainingCooldown > 0 && remainingCooldown <= cooldown - 1)
    //    {
    //        cooldownText.text = ((int)remainingCooldown + 1) + "";
    //        cooldownText.gameObject.SetActive(true);
    //        if (particleGraphics != null)
    //        {
    //            Destroy(particleGraphics);
    //        }
    //    }
    //    else
    //    {
    //        cooldownText.gameObject.SetActive(false);
    //        if (particleGraphics == null && isTracking)
    //        {
    //            particleGraphics = Instantiate(particlePrefab, transform);
    //        }
    //    }
    //}

    //public virtual void OnTrackingFound()
    //{
    //    Debug.Log("Tracking");
    //    Debug.Log("Create particle");
    //    if (remainingCooldown <= 0 && isPutDown)
    //    {
    //        attackParticleGraphics = Instantiate(attackParticlePrefab, transform);
    //        remainingCooldown = cooldown;
    //    }
    //    //particleGraphics = Instantiate(particlePrefab, transform);
    //    //TODO: Attack and cooldown here? It will not attack again if the card is not lost the tracking even if the cooldown is done.
    //    isPutDown = false;
    //    isTracking = true;
    //}

    //public virtual void OnTrackingLost()
    //{
    //    Debug.Log("Untracking");
    //    isPutDown = true;
    //    if (particleGraphics != null)
    //    {
    //        Debug.Log("Destroy particle");
    //        Destroy(particleGraphics);
    //        //TODO: Release cooldown or do something when it is gone?
    //        //TODO: Attack here
    //    }
    //    isTracking = false;
    //}

}
