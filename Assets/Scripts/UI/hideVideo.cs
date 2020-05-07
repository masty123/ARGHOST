using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Controlling deathscene.
public class hideVideo : MonoBehaviour
{
    [SerializeField] private Transform staticDeath;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {

        staticDeath = GameObject.FindGameObjectWithTag("staticDeath").transform;
        staticDeath.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
               staticDeath.gameObject.SetActive(true);
               //staticDeath.GetComponent<VideoScript>().playVideo();
        }
        else
        {
            staticDeath.gameObject.SetActive(false);
           // staticDeath.GetComponent<VideoScript>().stopVideo();
        }
    }
}
