using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webCamScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject webCameraPlane;
    void Start()
    {
        WebCamTexture webCameraTexture = new WebCamTexture();
        webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
        webCameraTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
