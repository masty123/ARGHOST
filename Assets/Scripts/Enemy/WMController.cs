using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WMController : IndependentEnemyController
{
    Renderer m_Renderer;

    private bool firstSaw;

    [SerializeField] private float stareTime = 3.5f;
    private float countTime;
    string seconds = "";

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        animator = GetComponentInChildren<Animator>();
        m_Renderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GhostBeHavior();
    } 

    public override void GhostBeHavior()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        if (m_Renderer.isVisible && !firstSaw)
        {
            firstSaw = true;
            animator.SetBool("lookAway", false);
            Debug.Log("Object is visible");
            stareTimer();
        }
        else if (!m_Renderer.isVisible && firstSaw)
        {
            Debug.Log("Object is no longer visible");
            animator.SetBool("lookAway", true);
            huntPlayer();
            //Jumpscare.
        }

        else if (m_Renderer.isVisible && firstSaw)
        {
            Debug.Log("Object is visible");
            //Jumpscare.
            animator.SetBool("lookAway", false);
            stareTimer();

        }
    }

    void huntPlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        //Move to Player
        transform.position += transform.forward * maxSpeed * Time.deltaTime;
        //if got hit or touch player.
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

    void stareTimer()
    {
        if(countTime >= 0)
        {
            countTime = stareTime - Time.time;
            seconds = (countTime % 60).ToString("f2");
        }
        else
        {
            StartCoroutine(dying());
        }
 
    }


}
