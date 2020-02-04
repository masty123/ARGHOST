using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using GoogleARCoreInternal;
using UnityEngine;

public class BaseCard : AugmentedImageVisualizer
{

    //public AugmentedImage Image;

    public GameObject particlePrefab;

    private GameObject particleGraphics;

    /*
    /// <summary>
    /// The Unity Update method.
    /// </summary>
    public void Update()
    {
        if (Image != null)
        {
            Debug.Log(Image.TrackingMethod.ToString());
            if (Image.TrackingState == TrackingState.Tracking && Image.TrackingMethod == AugmentedImageTrackingMethod.FullTracking)
            {
                if (particleGraphics == null)
                {
                    particleGraphics = Instantiate(particlePrefab, transform);
                }
            }
            else
            {
                if(particleGraphics != null)
                {
                    Destroy(particleGraphics.gameObject);
                }
            }
        }
        else
        {
            if (particleGraphics != null)
            {
                Destroy(particleGraphics.gameObject);
            }
        }

    }
    */

    public void OnDetected()
    {
        VDebug.Instance.Log("Base card: OnDetected got called");
        if (particleGraphics == null)
        {
            VDebug.Instance.Log("Base card: ParticleGraphics is not null and instantiating");
            particleGraphics = Instantiate(particlePrefab, transform);
        }
    }

    public void OnUndetected()
    {
        VDebug.Instance.Log("Base card: OnUndetected got called");
        if (particleGraphics != null)
        {
            VDebug.Instance.Log("Base card: ParticleGraphics is not null and destroying");
            Destroy(particleGraphics.gameObject);
        }
    }

}
