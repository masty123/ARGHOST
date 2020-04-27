using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
* New Enemy Controller without navigation mesh. can be act as a flying enemy if not spawning on 0 Y-Axis
*
*/
public class IndependentEnemyController : MonoBehaviour
{
    protected Transform player;

    [Header("AI Speed")]
    //rotation speed
    public float rotationSpeed = 3.0f;
    //max movement speed
    public float maxSpeed = 3.0f;
    //movement speed
    public float moveSpeed;
    //acecelerate speed
    public float accelSpeed;
    //time before max speed
    public float timeZeroToMaxSpeed = 2.5f;


    // animation max speed
    public float animatorMaxSpeed = 1.75f;
    //animation speed
    public float animatorSpeed;
    //acecelerate animation speed
    public float animatorAcelSpeed;
    //time before max speed
    public float timeZeroToAnim = 2.5f;


    //vision radius of enemy.
    [Header("AI Vision")]
    public float lookRadius = 10f;

    [Header("AI Behaviors")]
    //check if enemy catch the player.
    public bool EnteredTrigger;
    //check if player show cross.
    public bool showCross;

    public int Gold = 1;



    [Header("Dying Behaviors")]
    ////Time before destroying the prefab
    [SerializeField] private float deathTime = 0.55f;

    //Checking if got hit by player.
    public bool isHit;

    public bool isOutPortal = false;
    protected bool isRandom = false;
    protected int randomPattern;
    public hideVideo hidVidObj;
    public AudioSource jumpScare;


    [Header("Particle Effect")]
    [SerializeField] protected GameObject defeatParticlePrefab;
    protected GameObject defeatParticleGraphics;
    protected Animator animator;

    private void Awake()
    {
        accelSpeed = maxSpeed / timeZeroToMaxSpeed;
        moveSpeed = 0f;
        animatorAcelSpeed = animatorMaxSpeed / timeZeroToAnim;
        animatorSpeed = 0.5f;

    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        animator = GetComponentInChildren<Animator>();
        hidVidObj = GameObject.FindGameObjectWithTag("hideVidSwitch").transform.GetComponent<hideVideo>();
        jumpScare = GameObject.FindGameObjectWithTag("jumpScareAudio").GetComponentInChildren<AudioSource>();


        jumpScare.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        GhostBeHavior();
    }

    //Behavior of the ghost such as coming out of the portal, tracking player, dying, and etc.
    public virtual void GhostBeHavior()
    {
        if (isOutPortal)
        {
            if (!isRandom)
            {
                randomPattern = Random.Range(0, 2);
                Debug.Log(randomPattern.ToString());
                isRandom = true;
            }

            animator.SetBool("isSpawning", true);
            animator.SetBool("isRunning", true);
            animator.SetInteger("isRunningInt", randomPattern);
           if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                //Look at player
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
                //Move to Player
                if(moveSpeed < maxSpeed)
                {   
                    moveSpeed += accelSpeed * Time.deltaTime;
                    animatorSpeed += animatorAcelSpeed * Time.deltaTime;
                }
                animator.SetFloat("accelSpd", animatorSpeed);
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

    //Ghost got killed.
     public void Defeat()
    {
        ScoreManager.KilledGhosts += 1;
        UserInfo.savefile.Gold += Gold;
        if (showCross)
        {
            //react something.
            DeadEffect();
        }
    }

    //count down before destroying itself.
    public IEnumerator dying()
    {
        enemyHurt();
        yield return new WaitForSeconds(deathTime);
        DeadEffect();
    }

    //Jumpscare the player
    public virtual IEnumerator scare()
    {
        enemyScare();
        yield return new WaitForSeconds(1.25f);
        StartCoroutine(LoadAsynchronously("GameOver"));
    }


    //load the scene, asynchronously
    public  IEnumerator LoadAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {

            if (operation.progress == 0.9f && hidVidObj != null)
            {
                hidVidObj.isActive = true;
                yield return new WaitForSeconds(2.5f);
                operation.allowSceneActivation = true;

            }
            yield return null;
        }
    }


    // Effect play when this enemy is dead.
    void DeadEffect()
    {
        defeatParticleGraphics = Instantiate(defeatParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);                    // destroy this enemy
        Destroy(defeatParticleGraphics, 1.5f);  // destroy particle object
    }

    // Hurting like hell!
    public void enemyHurt()
    {
        maxSpeed = 0;
        animator.SetBool("isHurt", true);
    }

    //Set jumpscare bool in the animation controller
    public void enemyScare()
    {
        maxSpeed = 0;
        animator.SetBool("isTriggerScare", true);
        jumpScare.gameObject.SetActive(true);

    }

    //do something when enemy hit player (Orignally jumpscare) right now, kill themself.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("MainCamera"))
        {
            EnteredTrigger = true;
           //scare();
        }
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

    //manually shake the enemy.
    // transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x + left, transform.localPosition.y, transform.localPosition.z),
    //                                        new Vector3(transform.localPosition.x + right, transform.localPosition.y, transform.localPosition.z),
    //                                       (Mathf.Sin(shakeSpeed * Time.time) + 1.0f) / shakeRate);
}
