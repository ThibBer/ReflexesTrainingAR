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
        Debug.Log(GameObject.Find("ConfigurationManager").GetComponent<ConfigurationManager>().getGameTime_seconds());
        if (GameObject.Find("ConfigurationManager").GetComponent<ConfigurationManager>().getGameTime_seconds() == gameTime_seconds)
        {
            GetComponent<Image>().color = Color.blue;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
