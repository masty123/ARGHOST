using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public Vector3 speed;
    // Start is called before the first frame update
    void Start()
    {
        //portalComp = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
