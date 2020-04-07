using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(UserInfo.savefile != null)
        {
            GetComponent<Text>().text = UserInfo.savefile.PlayerName + ": " + UserInfo.savefile.HighScore;
        }
        else
        {
            GetComponent<Text>().text = "Debug player: 3000";
        }
    }

}
