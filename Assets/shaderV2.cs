using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderV2 : MonoBehaviour
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
                foreach (Transform child in transform)
                {
                    if (child.tag.Equals("EnemyBody"))
                    {
                        if (child.name.Equals("7_almahairs_0_3_0"))
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
