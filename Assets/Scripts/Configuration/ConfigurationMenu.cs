using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationMenu : MonoBehaviour
{
    private ConfigurationDatabase m_ConfigDB;

    public void Start()
    {
        m_ConfigDB = ConfigurationDatabase.Instance;
    }

    public void SetGameTimeSeconds(int seconds)
    {
        m_ConfigDB.GameTimeSeconds = seconds;
    }
}