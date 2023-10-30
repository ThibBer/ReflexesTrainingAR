using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class ButtonsManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Button button;
    private Button m_CurrentButton;
    #endregion

    #region Properties
    public int Generated { get; private set; }
    public float Distance { get; private set; }
    #endregion

    #region Methods

    public void GenerateNextButton()
    {
        var x = Camera.main.transform.position.x;
        var y = Camera.main.transform.position.y;
        var btn = Instantiate(button, new Vector3(Random.Range(x - 5f, x + 5f), Random.Range(x - 5f, x + 5f), 20), button.transform.rotation); // TODO: define ranges

        if (m_CurrentButton == null)
        {
            m_CurrentButton = btn;
        }

        Generated++;
        Distance += m_CurrentButton != null ? Vector3.Distance(m_CurrentButton.transform.position, btn.transform.position) : 0;
        btn.IsActive = true;
        m_CurrentButton = btn;
    }

    public void RemoveLast()
    {
        m_CurrentButton.IsActive = false;
    }

    private void Start()
    {
        GenerateNextButton();
        button.IsActive = false; // Template button: disable/hide it in another way
    }
    #endregion
}