using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static bool SaveData(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

        FileStream stream = File.Create(path);

        formatter.Serialize(stream, saveData);
        stream.Close();

        return true;
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }

    public static object LoadData(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream stream = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(stream);
            stream.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat($"Failed to load file at {path}");
            stream.Close();
            return null;
        }
    }
}
