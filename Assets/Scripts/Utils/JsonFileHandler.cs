using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows;

public class JsonFileHandler<T> : IFileHandlerStrategy<T> {

    private readonly string m_Path;

    public JsonFileHandler(string path)
    {
        m_Path = path;
    }

    public T ReadData()
    {
        var json = string.Empty;

        // File: Use UnityEngine.Windows instead of System.IO for the Hololens 1
        if(File.Exists(m_Path))
        {
            var jsonBytes = File.ReadAllBytes(m_Path);
            json = Encoding.UTF8.GetString(jsonBytes, 0, jsonBytes.Length);
            Debug.Log($"Read: {json}");
        }

        return JsonUtility.FromJson<T>(json);
    }

    public void SaveData(T dataList)
    {
        var json = JsonUtility.ToJson(dataList);
        File.WriteAllBytes(m_Path, Encoding.UTF8.GetBytes(json));
        Debug.Log($"Saved: {json}");
    }
}
