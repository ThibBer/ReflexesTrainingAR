using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour
{
    public int gameTime_seconds;

    // Update is called once per frame
    void Update()
    {
        if (ConfigurationDatabase.getInstance().gameTime_seconds == gameTime_seconds)
        {
            GetComponent<Image>().color = Color.blue;
            GetComponentInChildren<Text>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            GetComponentInChildren<Text>().color = Color.black;
        }
    }
}
