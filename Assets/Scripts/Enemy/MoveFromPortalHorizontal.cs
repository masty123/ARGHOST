using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromPortalHorizontal : MonoBehaviour
{
    public bool outPortal;
    private Transform childGhost;
    public ParticleSystem particleGameObject;
    public GameObject[] blackholeGameObject;
    [SerializeField] float height = 2f;

    private void Start()
    {
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
            if (childGhost.transform.localPosition.y < height)
            {
                float speed = childGhost.GetComponentInParent<IndependentEnemyController>().moveSpeed / 8;
                childGhost.transform.position += childGhost.transform.up * speed  * Time.deltaTime;
                outPortal = false;
            }
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
