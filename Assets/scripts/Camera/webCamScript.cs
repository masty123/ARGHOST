using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webCamScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject webCameraPlane;
    void Start()
    {
        //Check if its a mobile platform
        if(Application.isMobilePlatform)
        {
            //Create new gameobject called camera parent
            GameObject cameraParent = new GameObject("camParent");
            //Set cameraParent position as the same as this transform.
            cameraParent.transform.position = this.transform.position;
            //parent relationship
            this.transform.parent = cameraParent.transform;
            //camera rotation
            cameraParent.transform.Rotate(Vector3.right, 90);
        }

        Input.gyro.enabled = true;


        WebCamTexture webCameraTexture = new WebCamTexture();
        webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
        webCameraTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion as gyro rotation;
        Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, Input.gyro.attitude.z, -Input.gyro.attitude.w);
        this.transform.localRotation = cameraRotation;
    }
}
