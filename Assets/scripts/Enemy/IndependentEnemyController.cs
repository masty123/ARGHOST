using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentEnemyController : MonoBehaviour
{

    private Transform player;
    //rotation speed
    [SerializeField] private float rotationSpeed = 3.0f;
    //movement speed
    [SerializeField] private float moveSpeed = 3.0f;

    //vision radius of enemy.
    [Header("AI Vision")]
    public float lookRadius = 10f;

    [Header("AI Behaviors")]
    //check if enemy catch the player.
    public bool EnteredTrigger;
    //check if player show cross.
    public bool showCross;


    [Header("Dying Behaviors")]
    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float shakeRate;
    [SerializeField] private float deathTime;
    private float currentTime;
    public bool isHit;

    [Header("Particle Effect")]
    [SerializeField] protected GameObject defeatParticlePrefab;
    protected GameObject defeatParticleGraphics;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Look at player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);

        //Move to Player
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        if (isHit || EnteredTrigger)
        {
            StartCoroutine(dying());
        }
    }

    public void Defeat()
    {
        if (showCross)
        {
            //react something.
            defeatParticleGraphics = Instantiate(defeatParticlePrefab, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);   // destroy this enemy
            Destroy(defeatParticlePrefab, 1.5f);    // destroy particle object
        }
    }

    //count down before destroying itself.
    public IEnumerator dying()
    {
        enemyHurt();
        yield return new WaitForSeconds(deathTime);
        Destroy(transform.parent.gameObject);

    }

    // Hurting like hell!
    void enemyHurt()
    {

        //agent.enabled = false;
        moveSpeed = 0;
        transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x + left, transform.localPosition.y, transform.localPosition.z),
                                               new Vector3(transform.localPosition.x + right, transform.localPosition.y, transform.localPosition.z),
                                              (Mathf.Sin(shakeSpeed * Time.time) + 1.0f) / shakeRate);
    }

    //do something when enemy hit player (Orignally jumpscare) right now, kill themself.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("MainCamera"))
        {
            EnteredTrigger = true;
            dying();
            //Destroy(transform.parent.gameObject);
        }
    }
}
