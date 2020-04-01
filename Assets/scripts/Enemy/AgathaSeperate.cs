using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AgathaSeperate : IndependentEnemyController
{

    public float speed = 2f;
    public AgathaEnemyController mainController;
    public Transform target;

    [HideInInspector] public int animationInt = 0;
    
    bool isWalking = false;
    bool isStarted = false;

    public void StartWalking()
    {
        isStarted = true;
    }

    //Behavior of the ghost such as coming out of the portal, tracking player, dying, and etc.
    override public void GhostBeHavior()
    {
        if (isStarted)
        {
            if (!isWalking)
            {
                if (transform.position != target.position)
                {
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
                else
                {
                    isWalking = true;
                    mainController.isWalking = true;
                }
            }
            else
            {
                if (!isRandom)
                {
                    randomPattern = Random.Range(0, 2);
                    isRandom = true;
                }
                animator.SetBool("isSpawning", true);
                animator.SetBool("isRunning", true);
                animator.SetInteger("isRunningInt", animationInt);
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
                {
                    //Look at player
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
                    //Move to Player
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    //if got hit or touch the  player.
                    if (isHit)
                    {
                        StartCoroutine(dying());
                    }
                    else if (EnteredTrigger)
                    {
                        StartCoroutine(scare());
                        //StartCoroutine(dying());  // For testing only.
                    }
                }
            }
        }
    }

}
