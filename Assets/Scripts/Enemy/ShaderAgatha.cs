using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//All shader need to be changed after coming out of the portal 
//This one is for Agatha
public class ShaderAgatha : MonoBehaviour
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
        ChangeShader();
    }

    void ChangeShader()
    {
        if (GetComponentInParent<IndependentEnemyController>().isOutPortal)
        {
            foreach (Transform child in transform)
            {
                if (child.tag.Equals("EnemyBody"))
                {
                    if (child.name.Equals("6_mesh1_0.1_16_16"))
                    {
                        child.GetComponent<Renderer>().material.shader = shader;
                        child.GetComponent<Renderer>().material.SetFloat("_Mode", 2); //Fade
                        child.GetComponent<Renderer>().material.SetInt("_ZWrite", 1);
                        child.GetComponent<Renderer>().material.EnableKeyword("_ALPHATEST_ON");

                    }
                    else
                    {
                        child.GetComponent<Renderer>().material.shader = shader;
                    }
                }
            }
        }

    }
}
