using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{

    public Transform targetObj;
    public int speed = 5;
    public Vector3 offsets;

    void Update()
    {
        if(targetObj != null)
        {
            var targetRotation = Quaternion.LookRotation((targetObj.transform.position + offsets) - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

}
