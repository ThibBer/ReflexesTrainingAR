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
        if (ConfigurationDatabase.Instance.GameTimeSeconds == gameTime_seconds)
        {
            GetComponent<Image>().color = ToColor(41471);
            GetComponentInChildren<Text>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            GetComponentInChildren<Text>().color = Color.black;
        }
    }

    private Color32 ToColor(int HexVal)
    {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return new Color32(R, G, B, 255);
    }
}
