using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject enemy;
    private bool isEnemyActive;

    //show status if the enemy is visible
    public bool isVisible;

    //Renderer m_renderer;


    // Start is called before the first frame update
    void Start()
    {
        //m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if(enemy == null)
        {
            //Add enemy into the enemy gameObject.
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            isEnemyActive = true;
        }
        else
        {
            Debug.Log("don't see any ghost...");
        }

        
        //if (m_renderer.isVisible)
        //{
        //    Debug.Log("Object is visible");
        //    isVisible = true;
        //}
        //else
        //{
        //    isVisible = false;
        //    Debug.Log("Object is no longer visible");
        //}
    }
}
