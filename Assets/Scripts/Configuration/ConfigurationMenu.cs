using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour
{
    private ConfigurationDatabase m_ConfigDB;

    public void Start()
    {
        m_ConfigDB = ConfigurationDatabase.Instance;
    }

    public void SetGameTime_seconds(int seconds)
    {
        m_ConfigDB.GameTimeSeconds = seconds;
    }
}