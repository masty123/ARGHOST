using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
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


    [Header("AI Movement")]
    //Attach player
    Transform target;
    //for enemy moving.
    NavMeshAgent agent ;
    private Rigidbody rb;





    // Start is called before the first frame update
    void Start()
    {   
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        currentTime = deathTime;
    }

    // Update is called once per frame
    void Update()
    {   
        

        if(target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    //Attack the target.
                    FaceTarget();
                }
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

    public void Defeat()
    {
        if(showCross)
        {
           //react something.
        }
    }

    public IEnumerator dying()
    {
        enemyHurt();
        yield return new WaitForSeconds(deathTime);
        Destroy(transform.parent.gameObject);

    }

    void enemyHurt()
    {       

        agent.isStopped = true;
        transform.position = Vector3.Lerp(new Vector3(left, transform.position.y, 0), new Vector3(right, transform.position.y, 0), (Mathf.Sin(shakeSpeed * Time.time) + 1.0f) / shakeRate);
        shakeSpeed += 15 * Time.deltaTime;
        shakeRate += 4 * Time.deltaTime;

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
