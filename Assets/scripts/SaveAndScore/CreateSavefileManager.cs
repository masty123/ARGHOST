using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CreateSavefileManager : MonoBehaviour
{

    [SerializeField] InputField nameText;

    public void OnSelectName()
    {
        UserInfo.savefile = new Savefile();
        UserInfo.savefile.PlayerName = nameText.text;
        SaveManager.Instance.CreateSave();
        SceneManager.LoadScene("MainMenu");
    }

}
