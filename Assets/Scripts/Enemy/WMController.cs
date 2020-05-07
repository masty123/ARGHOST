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

    private AudioSource stunSound;
    private AudioSource footSound;


    public override void Start()
    {
        countTime = stareTime;
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        animator = GetComponentInChildren<Animator>();
        m_Renderer = GetComponentInChildren<Renderer>();
        scareSystem = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<jumpScareV2>();
        hidVidObj = GameObject.FindGameObjectWithTag("hideVidSwitch").transform.GetComponent<hideVideo>();
        stunSound = GameObject.FindGameObjectWithTag("stun").GetComponentInChildren<AudioSource>();
        footSound = GameObject.FindGameObjectWithTag("footstep").GetComponentInChildren<AudioSource>();
        jumpScare = GameObject.FindGameObjectWithTag("jumpScareAudio").GetComponentInChildren<AudioSource>();


        stunSound.gameObject.SetActive(false);
        footSound.gameObject.SetActive(false);
        jumpScare.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        GhostBeHavior();

        if (EnteredTrigger)
        {
            countTime = 999f;
            stunSound.gameObject.SetActive(false);
            footSound.gameObject.SetActive(false);
            StartCoroutine(scare());
        }

        if (scareSystem.jumpScare)
        {
            moveSpeed = 0;
        }

    } 

    public override void GhostBeHavior()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        if (m_Renderer.isVisible && !firstSaw)
        {
            firstSaw = true;
            animator.SetBool("lookAway", false);
            //Debug.Log("Object is visible");
            stareTimer();

            stunSound.gameObject.SetActive(true);
            footSound.gameObject.SetActive(false);


        }
        else if (!m_Renderer.isVisible && firstSaw)
        {
            //Debug.Log("Object is no longer visible");
            animator.SetBool("lookAway", true);
            huntPlayer();

            stunSound.gameObject.SetActive(false);
        }

        else if (m_Renderer.isVisible && firstSaw)
        {
            //Debug.Log("Object is visible");
            animator.SetBool("lookAway", false);
            stareTimer();

            stunSound.gameObject.SetActive(true);
            footSound.gameObject.SetActive(false);


        }
    }

    public override IEnumerator scare()
    {
        enemyScare();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LoadAsynchronously("GameOver"));
    }


    void huntPlayer()
    {
        footSound.gameObject.SetActive(true);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        //Move to Player
        transform.position += transform.forward * maxSpeed * Time.deltaTime;

       
    }

    void stareTimer()
    {
        if(countTime >= 0)
        {
            countTime = countTime - Time.deltaTime;
            seconds = (countTime % 60).ToString("f2");
        }
        else
        {
            StartCoroutine(dying());
        }
 
    }


}
