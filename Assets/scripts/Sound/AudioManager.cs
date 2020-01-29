 using UnityEngine.Audio;
using UnityEngine;
using System;

//For Ambient, menu theme, non object related only.
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //Do some ambient scary theme I guess.
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: "+ name+" not found!");
            return;
        }
           
        s.source.Play();
    }

    private void Update()
    {
        
    }
}
