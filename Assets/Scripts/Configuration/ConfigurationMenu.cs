using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour
{
    public void setGameTime_seconds(int seconds)
    {
        ConfigurationDatabase.Instance.GameTimeSeconds = seconds;
    }
}