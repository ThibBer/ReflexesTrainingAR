using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationDatabase : MonoBehaviour
{
    private static ConfigurationDatabase Instance;
    public int GameTimeSeconds { get; set; } = 15;

    public static ConfigurationDatabase getInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
