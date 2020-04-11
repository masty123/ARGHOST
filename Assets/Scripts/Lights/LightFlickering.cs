using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Light flickering in the main menu nothing much.
public class LightFlickering : MonoBehaviour
{
    //light transform
    private Light compLight;
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

    //Random ghost pose
    private int randomPose;
    private Animator animator;


    // Start is called before the first frame update.
    void Start()
    {
        Timer = Random.Range(minTime, maxTime);
        source.time = audioTime;
        animator = entity.GetComponent<Animator>();
        compLight = transform.GetComponent<Light>();
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
            compLight.enabled = !compLight.enabled;
            if (isVisible() && compLight.enabled)
            {
                //randomPose = Random.Range(0, 3);
                //Debug.Log(randomPose.ToString());
                animator.SetInteger("Pose", randomPose);
                foreach (Transform child in entity)
                {   
                    if(child.transform.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        child.transform.GetComponent<SkinnedMeshRenderer>().enabled = true;
                    }
                }
            }
            else
            {
                randomPose = Random.Range(0, 3);
                Debug.Log(randomPose.ToString());
                animator.SetInteger("Pose", randomPose);
                foreach (Transform child in entity)
                {
                    if (child.transform.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        child.transform.GetComponent<SkinnedMeshRenderer>().enabled = false;
                    }
                }
            }


            Timer = Random.Range(minTime, maxTime);
            
            if(lightAudio != null && compLight.enabled)
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
