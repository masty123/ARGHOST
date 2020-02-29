using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focusCameraScare : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private bool isDead;
    private float searchCountdown = 1f;



    // Update is called once per frame
    void LateUpdate()
    {
        if(isDead)
        {
            lookAtGhost();
        }
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Enemy");

        }
        else if (target != null)
        {
            //Debug.Log("Do nothing");
        }
    }


    //Jumpscare
    void lookAtGhost()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target.transform);
    }

    bool isAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (target == null)
            {
                return false;
            }
        }
        return true;
    }

    //Check if enemy hit player's box collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            Debug.Log("look at ghost");
            isDead = true;
        }
    }
}
