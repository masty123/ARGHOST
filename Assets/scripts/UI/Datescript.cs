using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Datescript : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        string old = System.DateTime.Now.ToLocalTime().ToString("yyyy");
        int oldInt = int.Parse(old);
        while(oldInt != 1987)
        {
            oldInt -= 1;
        }
        string time = System.DateTime.Now.ToLocalTime().ToString("MMM. dd");
        text.text = time+" "+oldInt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
