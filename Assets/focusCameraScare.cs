using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focusCameraScare : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;



    // Update is called once per frame
    void Update()
    {
        getTarget();
    }

     void getTarget()
    {
        if(target == null)
        {   
            target = GameObject.FindGameObjectWithTag("Enemy").transform;
        }
    }

    void lookAtGhost()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            lookAtGhost();
        }
    }
}
