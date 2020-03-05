using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScareV2 : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject ghostObj;

    public bool jumpScare;
    public float distance;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

     
    void Update()
    {
        if (ghostObj == null)
        {
            ghostObj = GameObject.FindGameObjectWithTag("Enemy");
        }
        else if (ghostObj != null)
        {
            //Debug.Log("Do nothing");
            if (jumpScare)
            {
                startScare(ghostObj);
            }
        }
    }

  

    void startScare(GameObject ghost)
    {
        ghost.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + distance);
        ghost.transform.LookAt(mainCamera.transform);
        ghost.transform.parent = mainCamera.transform;
    }

    //Check if enemy hit player's box collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Debug.Log("look at ghost");
            jumpScare = true;
        }
    }


    void RotateObject()
    {

    }



    void ghostHide()
    {

    }
}
