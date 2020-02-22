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

    private Transform player;
    //rotation speed
    [SerializeField] private float rotationSpeed = 3.0f;
    //movement speed
    [SerializeField] public float moveSpeed = 3.0f;

    //vision radius of enemy.
    [Header("AI Vision")]
    public float lookRadius = 10f;

    [Header("AI Behaviors")]
    //check if enemy catch the player.
    public bool EnteredTrigger;
    //check if player show cross.
    public bool showCross;


    [Header("Dying Behaviors")]
    ////Shaking left
    //[SerializeField] private float left;
    ////Shaking right.
    //[SerializeField] private float right;
    ////Shaking speed.
    //[SerializeField] private float shakeSpeed;
    ////Shaking rate
    //[SerializeField] private float shakeRate;
    ////Time before destroying the prefab
    [SerializeField] private float deathTime;

    //Checking if got hit by player.
    public bool isHit;

    public bool isOutPortal = false;
    private bool isRandom = false;
    private int randomPattern;


    [Header("Particle Effect")]
    [SerializeField] protected GameObject defeatParticlePrefab;
    protected GameObject defeatParticleGraphics;


    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        GetAnimator();
    }

    void GetAnimator()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Alma"))
            {
                animator = child.gameObject.GetComponent<Animator>();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        GhostBeHavior();
    }

    void GhostBeHavior()
    {
        if (isOutPortal)
        {
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
                //Look at player
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
                //Move to Player
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                //if got hit or touch player.
                if (isHit)
                {
                    StartCoroutine(dying());
                }
                else if (EnteredTrigger)
                {
                    StartCoroutine(scare());
                    //StartCoroutine(dying());
                }
            }

        }
    }

     public void Defeat()
    {
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

    public IEnumerator scare()
    {

        enemyScare();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");

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
        moveSpeed = 0;
        animator.SetBool("isHurt", true);
       // transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x + left, transform.localPosition.y, transform.localPosition.z),
       //                                        new Vector3(transform.localPosition.x + right, transform.localPosition.y, transform.localPosition.z),
       //                                       (Mathf.Sin(shakeSpeed * Time.time) + 1.0f) / shakeRate);
    }

    public void enemyScare()
    {
        moveSpeed = 0;
        animator.SetBool("isTriggerScare", true);
    }

    //do something when enemy hit player (Orignally jumpscare) right now, kill themself.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("MainCamera"))
        {
            EnteredTrigger = true;
            scare();
        }
    }
}
