using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    /// <summary>
    /// Makes a new binary file.
    /// Opens the FileStream to save the passed in game data into the newly made file.
    /// Then closes the stream to prevent corruption.
    /// </summary>
    public static void Save(string fileName, GameData gd)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".dat";
        FileStream fs = new FileStream(path, FileMode.Create);
        bf.Serialize(fs, gd);

        // ALWAYS DO THIS
        fs.Close();
        Debug.Log(path);
    }

    /// <summary>
    /// Looks for a file, if it exists, open it.
    /// Gather the stores information then return what was found.
    /// If the file does not exist, return null.
    /// </summary>
    public static GameData Load(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".dat";
        if(File.Exists(path) == true)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            GameData gd = bf.Deserialize(fs) as GameData;

        // ALWAYS DO THIS
            fs.Close();
            return gd;
        }

        return null;
    }
}
