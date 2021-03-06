﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoreManager
{

    static float GHOST_SCORE_MULTIPLIER = 100f;
    static float TIMER_SCORE_MULTIPLIER = 1f;
    static float TIMER_SCORE_DEVIDER = 100f;

    public static int KilledGhosts = 0;
    public static float SurviveTime = 0f;

    public static void ResetScore()
    {
        KilledGhosts = 0;
        SurviveTime = 0f;
    }

    public static float GetScore()
    {
        return (KilledGhosts * GHOST_SCORE_MULTIPLIER)
            + ((SurviveTime * TIMER_SCORE_MULTIPLIER)/TIMER_SCORE_DEVIDER);
    }

    public static bool IsHighScore()
    {
        return GetScore() > UserInfo.savefile.HighScore;
    }

    public static float GetHighScore()
    {
        if (IsHighScore())
        {
            return GetScore();
        }
        else
        {
            return UserInfo.savefile.HighScore;
        }
    }

}
