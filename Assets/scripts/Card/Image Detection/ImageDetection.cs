using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using UnityEngine;

public class ImageDetection : MonoBehaviour
{

    /// <summary>
    /// A prefab for visualizing an AugmentedImage.
    /// </summary>
    public BaseCard AugmentedImageVisualizerPrefab;

    /// <summary>
    /// The overlay containing the fit to scan user guide.
    /// </summary>
    public GameObject FitToScanOverlay;

    protected Dictionary<int, BaseCard> m_Visualizers
        = new Dictionary<int, BaseCard>();

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    /// <summary>
    /// The Unity Awake() method.
    /// </summary>
    public void Awake()
    {
        // Enable ARCore to target 60fps camera capture frame rate on supported devices.
        // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// The Unity Update method.
    /// </summary>
    public void Update()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        // Get updated augmented images for this frame.
        Session.GetTrackables<AugmentedImage>(
            m_TempAugmentedImages, TrackableQueryFilter.Updated);

        // Create visualizers and anchors for updated augmented images that are tracking and do
        // not previously have a visualizer. Remove visualizers for stopped images.
        foreach (var image in m_TempAugmentedImages)
        {
            BaseCard visualizer = null;
            m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
            if (image.TrackingState == TrackingState.Tracking && visualizer == null)
            {
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                visualizer = (BaseCard)Instantiate(
                    AugmentedImageVisualizerPrefab, anchor.transform);
                visualizer.Image = image;
                m_Visualizers.Add(image.DatabaseIndex, visualizer);
            }
            else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                m_Visualizers.Remove(image.DatabaseIndex);
                GameObject.Destroy(visualizer.gameObject);
            }
        }

        // Show the fit-to-scan overlay if there are no images that are Tracking.
        foreach (var visualizer in m_Visualizers.Values)
        {
            if (visualizer.Image.TrackingMethod == AugmentedImageTrackingMethod.FullTracking)
            {
                FitToScanOverlay.SetActive(false);
                //VDebug.Instance.Log("Card detection: Card got tracked in card detection");
                AugmentedImageVisualizerPrefab.OnDetected();
                return;
            }
            else if (visualizer.Image.TrackingMethod == AugmentedImageTrackingMethod.LastKnownPose)
            {
                VDebug.Instance.Log("Card detection: Card tracking method is last known position");
                AugmentedImageVisualizerPrefab.OnUndetected();
                FitToScanOverlay.SetActive(true);
            }
            else {
                VDebug.Instance.Log("Card detection: Card tracking method is not tranked");
                AugmentedImageVisualizerPrefab.OnUndetected();
                FitToScanOverlay.SetActive(true);
            }
        }

        //FitToScanOverlay.SetActive(true);
    }

}
