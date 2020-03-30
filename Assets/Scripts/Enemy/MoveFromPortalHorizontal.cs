using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is a script for moving enemy on horizontal axis.
public class MoveFromPortalHorizontal : MonoBehaviour
{
    public bool outPortal;
    private Transform childGhost;
    public ParticleSystem particleGameObject;
    public GameObject[] blackholeGameObject;
    [SerializeField] float height = 2f;

    private void Start()
    {
        //Find child enemy in the transform.
        foreach(Transform child in transform)
        {
            if(child.tag.Equals("Enemy"))
            {
                childGhost = child.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(childGhost != null)
        {   
            //Move the enemy from the portal.
            if (childGhost.transform.localPosition.y < height)
            {
                float speed = childGhost.GetComponentInParent<IndependentEnemyController>().moveSpeed / 8;
                childGhost.transform.position += childGhost.transform.up * speed  * Time.deltaTime;
                outPortal = false;
            }
            //Destroy portal and particle prefab after the enemy successfully came out of the portal
            else
            {   
                outPortal = true;
                childGhost.GetComponent<IndependentEnemyController>().isOutPortal = true;
                transform.DetachChildren();
                particleGameObject.Stop();
                foreach(GameObject child in blackholeGameObject)
                {
                    Destroy(child);
                }
                Destroy(gameObject);
            }
        }
    }
}
