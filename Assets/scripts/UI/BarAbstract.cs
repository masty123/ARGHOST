using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BarAbstract : MonoBehaviour
{

    [SerializeField] protected Image bar;
    [SerializeField] protected Text statText;

    protected float fillAmount;

    protected float current;
    protected float max;
    protected string statType = "Type";

    // Update is called once per frame
    void Update()
    {
        GetValue();
        HandleBar();
    }

    // get current and max value
    // override this method
    protected virtual void GetValue()
    {
        current = 100f;
        max = 100f;
    }

    protected void HandleBar()
    {
        bar.fillAmount = Map(current, 0, max, 0, 1);
        statText.text = String.Format("{0}: {1:0}/{2:0}", statType, current, max);
    }

    // Turn stat into fillAmount for content
    protected float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
