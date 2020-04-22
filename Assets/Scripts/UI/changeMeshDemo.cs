using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMeshDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.color = Color.yellow ;
        }
    }
}
