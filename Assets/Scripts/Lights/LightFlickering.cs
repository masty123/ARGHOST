using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    //light transform
    public Light light;
    // minimum and maxmium time that light stay on.
    public float minTime, maxTime;
    public float Timer;
    //source of the audio, duh
    public AudioSource source;
    //Audio clip
    public AudioClip lightAudio;
    //Ghost transform
    public Transform entity;
    //Audio time range
    public float audioTime;

    // Start is called before the first frame update.
    void Start()
    {
        Timer = Random.Range(minTime, maxTime);
        source.time = audioTime;
    }

    // Update is called once per frame.
    void Update()
    {
        flickeringLight();
    }

    //Randomize the entity appearance.
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
            
            if(lightAudio != null && light.enabled)
            {      
                source.PlayOneShot(lightAudio);        
            }
            else
            {
                source.Stop();
            }
        }
    }

    //Shoiuld the ghost appear.
    bool isVisible()
    {
        return (Random.value > 0.5f);
    }
}
