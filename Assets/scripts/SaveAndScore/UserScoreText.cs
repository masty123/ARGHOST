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
            string playerName = UserInfo.savefile.PlayerName.ToUpper();
            GetComponent<Text>().text = playerName + ": " + UserInfo.savefile.HighScore;
            GetComponent<Text>().text.ToUpper();
        }
        else
        {
            GetComponent<Text>().text = "DEBUG PLAYER: 3000";
        }
    }

}
