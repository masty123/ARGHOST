using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        Text text = GetComponent<Text>();

        if (ScoreManager.IsHighScore())
        {
            text.text = "NEW HIGH SCORE!\n" + (int)ScoreManager.GetScore();
            UserInfo.savefile.HighScore = ScoreManager.GetScore();
            HighScoreSystem hss = GameObject.FindObjectOfType<HighScoreSystem>();
            if (hss != null)
            {
                hss.AddNewHighscore(UserInfo.savefile.PlayerName, UserInfo.savefile.HighScore);
            }
        }
        else
        {
            text.text = "SCORE\n" + (int)ScoreManager.GetScore()
                + "\n\nHIGH SCORE\n" + (int)ScoreManager.GetHighScore();
        }

        UserInfo.savefile.overallGhostKilled += ScoreManager.KilledGhosts;
        UserInfo.savefile.overallTimeSurvived += ScoreManager.SurviveTime;

        SaveManager.Save(UserInfo.savefile);

    }

}
