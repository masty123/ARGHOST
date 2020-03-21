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
        string time = System.DateTime.Now.ToLocalTime().ToString("MMM. dd yyyy");
        text.text = time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
