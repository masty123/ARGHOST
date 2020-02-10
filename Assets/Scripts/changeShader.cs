using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeShader : MonoBehaviour
{

    private Shader shader;
    //Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        shader = Shader.Find("Standard");
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<IndependentEnemyController>().isOutPortal)
        {
            foreach(Transform child in transform)
            {
                if (child.tag.Equals("EnemyBody"))
                {
                    child.GetComponent<Renderer>().material.shader = shader;
                }
            }
        }
    }
}
