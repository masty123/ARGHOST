using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]

public class VideoScript : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer VideoPlayer;
    private Texture m_RawImageTexture;
    //public bool toggleVid;



    // Start is called before the first frame update
    void Start()
    {
        //VideoPlayer.enabled = false;
        //m_RawImageTexture = rawImage.texture;
    }

    // Update is called once per frame
    void Update()
    {   
        //For Testing purpose
    }

    public void playVideo()
    {
        VideoPlayer.enabled = true;
        VideoPlayer.Play();
    }

    public void stopVideo()
    {
        VideoPlayer.Stop();
        rawImage.texture = m_RawImageTexture;
        VideoPlayer.enabled = false;
    }
}
