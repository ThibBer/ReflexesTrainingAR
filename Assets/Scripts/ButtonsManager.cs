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
    /// <summary>
    /// Number of generated buttons so far
    /// </summary>
    public int GeneratedNumber { get; private set; }
    /// <summary>
    /// Total distance between all the generated buttons
    /// </summary>
    public float TotalDistance { get; private set; }
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

        GeneratedNumber++;
        TotalDistance += m_CurrentButton != null ? Vector3.Distance(m_CurrentButton.transform.position, btn.transform.position) : 0;
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