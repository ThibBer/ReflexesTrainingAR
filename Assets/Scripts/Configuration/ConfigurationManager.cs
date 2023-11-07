using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManager : MonoBehaviour
{
    private static ConfigurationManager Instance;
    private int gameTime_seconds = 15;

    public static ConfigurationManager getInstance()
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

    public int getGameTime_seconds()
    {
        return this.gameTime_seconds;
    }

    public void setGameTime_seconds(int seconds)
    {
        this.gameTime_seconds = seconds;
    }
}