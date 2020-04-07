using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    static string SAVE_FILE_NAME = "ARGhost_Savefile001.dat";

    static SaveManager instance;
    public static SaveManager Instance {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            else
            {
                instance = new SaveManager();
                return instance;
            }
        }
    }

    SaveManager()
    {

    }

    public static void Save(Savefile savefile)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + SAVE_FILE_NAME, FileMode.OpenOrCreate);
        bf.Serialize(file, savefile);
        file.Close();
    }

    public static bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SAVE_FILE_NAME))
        {
            Debug.Log("File does exist!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SAVE_FILE_NAME, FileMode.Open);
            UserInfo.savefile = (Savefile)bf.Deserialize(file);
            file.Close();
            return true;
        }
        else
        {
            Debug.Log("File does not exist!");
            return false;
        }
    }

    //This one is null until UserManager.cs created the user. Currently unused for now.
    public void CreateSave()
    {
        if (!Load())
        {
            if (UserInfo.savefile != null)
            {
                Debug.Log("Trying to create the new save file");
                Save(UserInfo.savefile);
            }
            else
            {
                Debug.Log("User is null");
            }
        }
    }

    //Return but not replace UserInfo. Temporary savefile loading.
    public Savefile TempLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SAVE_FILE_NAME))
        {
            Debug.Log("File does exist!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SAVE_FILE_NAME, FileMode.Open);
            Savefile savefile = (Savefile)bf.Deserialize(file);
            file.Close();
            return savefile;
        }
        else
        {
            Savefile savefile = new Savefile();
            savefile.PlayerName = "Corrupted Savefile";
            return savefile;
        }
    }

}
