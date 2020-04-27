using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script will teleport ghost at the front of the camera and attach it as a child of the camera when player got jumpscare.
public class jumpScareV2 : MonoBehaviour
{
    //public GameObject mainCamera;
    public GameObject ghostObj;

    public bool jumpScare;
    public float distance;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {
        //used to automatically find the camera but we attached it manually anyway.
        //mainCamera = GameObject.FindWithTag("MainCamera");
    }


    void Update()
    {   
        //Checks if the ghost is in the game or not
        //if not: find it
        //if in the game: checks jumpscare loop.
        //if (ghostObj == null)
        //{   
        //    ghostObj = GameObject.FindGameObjectWithTag("Enemy");
        //}
        //else if (ghostObj != null)
        //{
            if (jumpScare)
            {
                startScare(ghostObj);
            }
        //}
    }

  
    //Teleport ghost at the front of the camera and attach it as a child.
    void startScare(GameObject ghost)
    {
        ghost.transform.position = transform.position + transform.forward * distance;
        ghost.transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
        ghost.transform.LookAt(transform);
        ghost.transform.parent = transform;
    }

    //Check if enemy hit player's box collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Debug.Log("look at ghost");
            ghostObj = other.transform.gameObject;
            jumpScare = true;
        }
    }
}
