using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public struct Highscore
{
    public string username;
    public float score;

    public Highscore(string _username, float _score)
    {
        username = _username;
        score = _score;
    }
}

public class HighScoreSystem : MonoBehaviour
{

    const string privateCode = "Bvnh8McN1kOPH02tm0JeSAHb3yEaIcrEqFUZu5nMxtxg";
    const string publicCode = "5e8c13d0403c2d12b8c258f4";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;

    public Text highscoreBoardText;

    bool scoreDownloaded = false;

    private void Start()
    {
        DownloadHighscore();
    }

    private void Update()
    {
        if (scoreDownloaded && highscoreBoardText != null)
        {
            scoreDownloaded = false;

            for(int i = 0; i < highscoresList.Length; i++)
            {
                highscoreBoardText.text += (i+1) + "."
                    + highscoresList[i].username + "\n"
                    + highscoresList[i].score + "\n\n";
            }
        }
    }

    public void AddNewHighscore(string username, float score)
    {
        StartCoroutine(UploadNewHighScore(username, score));
    }

    public void DownloadHighscore()
    {
        StartCoroutine(DownloadHighScoreFromDatabase());
    }

    private void FormatHighscores(string Textstream)
    {
        string[] entries = Textstream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for(int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split('|');
            string username = entryInfo[0];
            float score = float.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
        }
        scoreDownloaded = true;
    }

    IEnumerator UploadNewHighScore(string username, float score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + (int)score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload successful");
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    IEnumerator DownloadHighScoreFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }

}