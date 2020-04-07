using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Savefile
{

    public string PlayerName = "";
    public int HighScore = 0;
    public int Gold = 0;
    public Dictionary<string,int> Upgrades;

    public int overallGhostKilled = 0;
    public int overallTimeSurvived = 0;

    public Savefile()
    {
        Upgrades = new Dictionary<string, int>();
    }

}
