using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

public static class SaveLoad
{
    public static void Save(string fileName, GameData gd)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".dat";
        FileStream fs = new FileStream(path, FileMode.Create);
        bf.Serialize(fs, gd);
        fs.Close();
        Debug.Log(path);
    }

    public static GameData Load(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".dat";
        if(File.Exists(path) == true)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            GameData gd = bf.Deserialize(fs) as GameData;
            fs.Close();
            return gd;
        }

        return null;
    }
}
