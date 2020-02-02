using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public Light light;

    public float minTime, maxTime;
    public float Timer;

    public AudioSource source;
    public AudioClip lightAudio;
    public Transform entity;
    // Start is called before the first frame update
    void Start()
    {
        Timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        flickeringLight();
    }

    void flickeringLight()
    {
        if (Timer > 0) Timer -= Time.deltaTime;

        if(Timer <= 0)
        {
            light.enabled = !light.enabled;
            if (isVisible() && light.enabled)
            {
                entity.transform.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                entity.transform.GetComponent<MeshRenderer>().enabled = false;
            }


            Timer = Random.Range(minTime, maxTime);
            
            if(lightAudio != null)
            {
                source.PlayOneShot(lightAudio);

            }
        }
    }
    bool isVisible()
    {
        return (Random.value > 0.5f);
    }
}
