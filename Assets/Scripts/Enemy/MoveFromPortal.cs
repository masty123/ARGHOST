using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromPortal : MonoBehaviour
{
    public bool outPortal;
    private Transform childGhost;
    public ParticleSystem particleGameObject;
    public GameObject blackholeGameObject;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.name.Equals("Enemy"))
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
            if (childGhost.transform.localPosition.y < 1)
            {
                childGhost.transform.position += childGhost.transform.up * childGhost.GetComponentInParent<IndependentEnemyController>().moveSpeed / 2 * Time.deltaTime;
                outPortal = false;
            }
            else
            {
                outPortal = true;
                childGhost.GetComponent<IndependentEnemyController>().isOutPortal = true;
                transform.DetachChildren();
                particleGameObject.Stop();
                Destroy(blackholeGameObject);
                Destroy(gameObject);
            }
        }
    }
}
