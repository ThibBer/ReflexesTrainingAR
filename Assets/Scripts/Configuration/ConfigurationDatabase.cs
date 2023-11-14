using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationDatabase : MonoBehaviour
{
    public static ConfigurationDatabase Instance { get; private set; }
    public int GameTimeSeconds { get; set; } = 15;

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
