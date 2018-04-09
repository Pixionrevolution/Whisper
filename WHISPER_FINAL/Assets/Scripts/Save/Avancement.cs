using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Avancement : MonoBehaviour
{
    public Save save;
    string directory = "/Ressources/";
    string filename = "save.json";

    void Awake()
    {
        try
        {
            string jsonFile = File.ReadAllText(Application.dataPath + directory + filename);
            save = Save.CreateFromJSON(jsonFile);
        }
        catch
        {
            save = new Save();
        }
    }

    void OnApplicationQuit()
    {
        if (save != null)
        {
            if (Directory.Exists(Application.dataPath + directory) == false)
            {
                Directory.CreateDirectory(Application.dataPath + directory);
            }
            Debug.Log(save.level);
            File.WriteAllText(Application.dataPath + directory + filename, save.ToJSON());
        }
    }
}