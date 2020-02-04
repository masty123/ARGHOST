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
        if (particleGraphics == null)
        {
            particleGraphics = Instantiate(particlePrefab, transform);
        }
    }

    public void OnUndetected()
    {
        if (particleGraphics != null)
        {
            Destroy(particleGraphics.gameObject);
        }
    }

}
