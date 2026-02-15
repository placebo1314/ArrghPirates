using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class DataSaver
{
    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public static void saveData<T>(T dataToSave, string dataFileName)
    {
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        //Convert To Json then to bytes
		string jsonData = JsonConvert.SerializeObject(dataToSave, Formatting.Indented, SerializerSettings);
        byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
        }

        try
        {
            File.WriteAllBytes(tempPath, jsonByte);
            Debug.Log("Saved Data to: " + tempPath.Replace("/", "\\"));
            Debug.Log(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To PlayerInfo Data to: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }
    
    public static T LoadData<T>(string dataFileName)
    {
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Debug.LogWarning("Directory does not exist");
            return default(T);
        }

        if (!File.Exists(tempPath))
        {
            Debug.Log("File does not exist");
            return default(T);
        }

        //Load saved Json
        byte[] jsonByte = null;
        try
        {
            jsonByte = File.ReadAllBytes(tempPath);
            Debug.Log("Loaded Data from: " + tempPath.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        //Convert to json string
        if (jsonByte == null || jsonByte.Length == 0)
        {
            return default(T);
        }

        string jsonData = Encoding.UTF8.GetString(jsonByte);
        Debug.Log("LoadedData : ");
        Debug.Log(jsonData);

        //Convert to Object
        //object resultValue = JsonUtility.FromJson<T>(jsonData);
        try
        {
            T result = JsonConvert.DeserializeObject<T>(jsonData, SerializerSettings);
            return result;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to deserialize '{dataFileName}'. Returning default value.");
            Debug.LogWarning("Error: " + e.Message);
            return default;
        }
        
        //return (T)Convert.ChangeType(resultValue, typeof(T));
    }

    public static bool deleteData(string dataFileName)
    {
        bool success = false;

        //Load Data
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Debug.LogWarning("Directory does not exist");
            return false;
        }

        if (!System.IO.File.Exists(tempPath))
        {
            Debug.Log("File does not exist");
            return false;
        }

        try
        {
            System.IO.File.Delete(tempPath);
            Debug.Log("Data deleted from: " + tempPath.Replace("/", "\\"));
            success = true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Delete Data: " + e.Message);
        }

        return success;
    }

    public static T LoadDataOrDefault<T>(string dataFileName, Func<T> fallbackFactory)
    {
        T loadedData = LoadData<T>(dataFileName);
        if (loadedData != null)
        {
            return loadedData;
        }

        if (fallbackFactory == null)
        {
            return default;
        }

        T fallback = fallbackFactory();
        Debug.LogWarning($"Using fallback data for '{dataFileName}'.");
        return fallback;
    }
    

[Serializable]
public class PlayerInfo
{
    public Dictionary<string, string> Ship1 = new Dictionary<string, string>();
    public Dictionary<string, string> Ship2 = new Dictionary<string, string>();
    
}

}
