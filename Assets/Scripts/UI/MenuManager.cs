using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
/*
*  Manage all the buttons in the main menu scene.
*/
public class MenuManager : MonoBehaviour
{   
    public GameObject menuCanvas;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Start ghost hunting.
    public void enterGame()
    {
        //Load game scene.
        SceneManager.LoadScene("Haunt");
        ScoreManager.ResetScore();
    }

    public void highscore()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
        ScoreManager.ResetScore();
    }

    //Might use something to login
    public void Login()
    {

    }

    //Config sound. Might have more options than this.
    public void Option()
    {

    }

    //Exit the Application.
    public void Quit()
    {
        Application.Quit();
    }

}
