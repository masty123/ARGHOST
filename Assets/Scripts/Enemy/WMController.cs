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
        hidVidObj = GameObject.FindGameObjectWithTag("hideVidSwitch").transform.GetComponent<hideVideo>();

    }

    // Update is called once per frame
    void Update()
    {
        GhostBeHavior();

        if (EnteredTrigger)
        {
            countTime = 999f;
            StartCoroutine(scare());
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
        }
        else if (!m_Renderer.isVisible && firstSaw)
        {
            //Debug.Log("Object is no longer visible");
            animator.SetBool("lookAway", true);
            huntPlayer();
        }

        else if (m_Renderer.isVisible && firstSaw)
        {
            //Debug.Log("Object is visible");
            animator.SetBool("lookAway", false);
            stareTimer();
        }
    }

    public override IEnumerator scare()
    {

        enemyScare();
        yield return new WaitForSeconds(1.5f);

        //StartCoroutine(playstaticDeath());
        //SceneManager.LoadScene("GameOver");
        StartCoroutine(LoadAsynchronously("GameOver"));
    }

    //Start playing static video after the player got jumpscared.
    //IEnumerator playstaticDeath()
    //{
    //    if(hidVidObj != null)
    //    {
    //        hidVidObj.isActive = true;
    //        yield return new WaitForSeconds(2.5f);
    //    }
    //}


    void huntPlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        //Move to Player
        transform.position += transform.forward * maxSpeed * Time.deltaTime;

       
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
