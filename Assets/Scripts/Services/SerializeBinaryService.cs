using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializeBinaryService
{
    public static void Save(object obj, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fStream = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(fStream, obj);
        fStream.Close();
    }

    public static bool CheckFileExists(string fileName)
    {
        return File.Exists(Application.persistentDataPath + fileName);
    }

    public static T Load<T>(ref T obj, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fStream = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
        obj = (T)bf.Deserialize(fStream);
        fStream.Close();
        return obj;
    }
}
