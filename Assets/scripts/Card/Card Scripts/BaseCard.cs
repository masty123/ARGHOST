using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using GoogleARCoreInternal;
using UnityEngine;

public class BaseCard : MonoBehaviour
{

    //public AugmentedImage Image;

    public GameObject particlePrefab;

    private GameObject particleGraphics;
    [HideInInspector] public Anchor anchor;

    public AugmentedImage Image;

    #region Backups
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
    #endregion

    private void Update()
    {
        if (particleGraphics != null)
        {
            VDebug.Instance.Log("Card particle wposition: " + particleGraphics.transform.position.x + " , " + particleGraphics.transform.position.y + " , " + particleGraphics.transform.position.z);
            VDebug.Instance.Log("Card particle lposition: " + particleGraphics.transform.localPosition.x + " , " + particleGraphics.transform.localPosition.y + " , " + particleGraphics.transform.localPosition.z);
        }
    }

    public void OnDetected()
    {
        //VDebug.Instance.Log("Base card: OnDetected got called");
        //Anchor anchor = Image.CreateAnchor(Image.CenterPose);
        if (particleGraphics == null)
        {
            VDebug.Instance.Log("Base card: ParticleGraphics is null and instantiating");
            particleGraphics = Instantiate(particlePrefab, transform);
        }
    }

    public void OnUndetected()
    {
        //VDebug.Instance.Log("Base card: OnUndetected got called");
        if (particleGraphics != null)
        {
            VDebug.Instance.Log("Base card: ParticleGraphics is not null and destroying");
            Destroy(particleGraphics.gameObject);
        }
    }

}
