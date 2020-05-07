using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgathaEnemyController : IndependentEnemyController
{

    public float outportalMaxSpeed = 0.5f;

    public float timeUntilSeperate = 2f;
    public AgathaSeperate seperateGhostPrefab;

    public Transform target1;
    public Transform target2;

    public bool isWalking = true;
    private bool isSeperated = false;

    //Behavior of the ghost such as coming out of the portal, tracking player, dying, and etc.
    override public void GhostBeHavior()
    {
        if (isOutPortal)
        {
            maxSpeed = outportalMaxSpeed;
            if (!isRandom)
            {
                randomPattern = Random.Range(0, 2);
                isRandom = true;
            }

            animator.SetBool("isSpawning", true);
            animator.SetBool("isRunning", true);
            animator.SetInteger("isRunningInt", randomPattern);

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                timeUntilSeperate -= Time.deltaTime;
                if(timeUntilSeperate <= 0 && !isSeperated && !isHit)
                {
                    //Set to false and wait until seperated ghosts call back
                    isWalking = false;
                    GameObject temp = Instantiate(seperateGhostPrefab.gameObject, transform);
                    GameObject temp2 = Instantiate(seperateGhostPrefab.gameObject, transform);
                    temp.transform.position = new Vector3(temp.transform.position.x, target1.position.y, temp.transform.position.z);
                    temp2.transform.position = new Vector3(temp2.transform.position.x, target2.position.y, temp2.transform.position.z);
                    AgathaSeperate tempScript = temp.GetComponent<AgathaSeperate>();
                    AgathaSeperate tempScript2 = temp2.GetComponent<AgathaSeperate>();
                    tempScript.target = target1;
                    tempScript2.target = target2;
                    tempScript.mainController = this;
                    tempScript2.mainController = this;
                    temp.transform.parent = null;
                    temp2.transform.parent = null;
                    tempScript.animationInt = randomPattern;
                    tempScript2.animationInt = randomPattern;
                    tempScript.StartWalking();
                    tempScript2.StartWalking();
                    isSeperated = true;
                }
                else if (isWalking)
                {
                    //Look at player
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
                    //Move to Player
                    transform.position += transform.forward * maxSpeed * Time.deltaTime;
                }
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
