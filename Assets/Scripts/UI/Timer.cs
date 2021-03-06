﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Timer for checking if the player survive.
public class Timer : MonoBehaviour
{

    private Text timerText;
    private float startTime;

    private bool isStarted = false;

    private void Start()
    {   
        timerText = this.GetComponent<Text>();
    }

    public void StartTimer()
	{
        startTime = Time.time;
        isStarted = true;
    }

    private void Update()
    {
		if (isStarted)
		{
            float t = Time.time - startTime;
            string minutes = (((int)t / 60) + 3).ToString();
            //string seconds = (t % 60).ToString("f2");

            timerText.text = minutes + " AM";

            ScoreManager.SurviveTime += t;

            if (minutes.Equals("6"))
            {
                SceneManager.LoadScene("Survive");
            }
        }
    }

}
