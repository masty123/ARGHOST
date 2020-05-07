using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using GoogleARCoreInternal;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour
{

    public AugmentedImage Image;

    public abstract void OnDetected();

    public abstract void OnUndetected();

}
