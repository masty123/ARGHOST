using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] Text timeText;
    float targetTime = 180f;    // 3 minutes = 180 seconds
    [SerializeField] public bool isCountdown = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
        
        float minutes = (float)Math.Floor( targetTime / 60f);
        float leaveSeconds = targetTime % 60f;
        timeText.text = String.Format("{0:0}:{1:00.00}", minutes, leaveSeconds);

        if(targetTime <= 0f) {
            isCountdown = false;
            targetTime = 0f;
            GameOver();
        }
    }

    void Countdown() {
        if(isCountdown ) {
            targetTime -= Time.deltaTime;
        }
    }

    void GameOver() {
        Debug.Log("Game Over! Time Over!");
    }
}
