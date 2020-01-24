﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //vision radius of enemy.
    public float lookRadius = 10f;
    //check if enemy is a trigger box collider.
    public bool EnteredTrigger;
   
    Transform target;
    NavMeshAgent agent ;




    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                //Attack the target.
                FaceTarget();
            }
        }
    }

    //Enemy turn their face at player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //display range of enemy
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    //do something when enemy hit player (Orignally jumpscare) right now, kill themself.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("MainCamera"))
        {
            EnteredTrigger = true;
            Destroy(transform.parent.gameObject);
        }
    }
}
