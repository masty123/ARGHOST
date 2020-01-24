using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0));
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                Destroy(Hit.transform.parent.gameObject);
                //Debug.Log(Hit.transform.tag.ToString());

            }
            else Debug.Log("can't see 'em...");
        }

    }

    void pushGhost()
    {

    }
}
