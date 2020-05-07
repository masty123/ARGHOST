using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCreatedChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!SaveManager.Load())
        {
            SceneManager.LoadScene("Create Savefile");
        }
    }

}
