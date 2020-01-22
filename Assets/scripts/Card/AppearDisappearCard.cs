using UnityEngine;
using Vuforia;

public class AppearDisappearCard : Card
{

    public override void OnTrackingFound()
    {
        Debug.Log("Tracking");
        Debug.Log("Create particle");
        particleGraphics = Instantiate(particlePrefab, transform);
        //TODO: Attack and cooldown here? It will not attack again if the card is not lost the tracking even if the cooldown is done.
    }

    public override void OnTrackingLost()
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
