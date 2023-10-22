using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Random = UnityEngine.Random;

public class ButtonsManager : MonoBehaviour
{
    #region Fields
    // TODO: Get ranges for every level later
    private const int NumberOfButtons = 5;

    // Unity property, camelCase needed
    [SerializeField]
    private Button button;

    private GestureRecognizer m_GestureRecognizer;

    /* 
     * Using a List with an index over an Enumerator, will be more convenient for distance evaluation
     * without updating the score everytime. Move to another class later if needed.
     */
    private IList<Button> m_Buttons;
    private int m_CurrentIdx = 0;

    #endregion

    #region Constructors

    #endregion

    #region Properties

    #endregion

    #region Methods

    private void Awake()
    {
        m_GestureRecognizer = new GestureRecognizer();
        m_GestureRecognizer.StartCapturingGestures();
        m_GestureRecognizer.Tapped += OnTapped;
    }


    private void GenerateButtons()
    {
        m_Buttons = new List<Button>();
        for (var i = 0; i < NumberOfButtons; i++)
        {
            var x = Camera.main.transform.position.x;
            var y = Camera.main.transform.position.y;
            // TODO: change Range values (difficulty level)
            var btn = Instantiate(button, new Vector3(Random.Range(x - 5f, x + 5f), Random.Range(x - 5f, x + 5f), 20), Quaternion.identity);
            btn.transform.SetParent(transform, false);
            btn.IsActive = false; // Explicitly disabled first
            m_Buttons.Add(btn);
        }
    }

    private void Start()
    {
        GenerateButtons();
        button.IsActive = false; // Template button: disable/hide it in another way

        if (m_Buttons.Count < 2)
        {
            throw new Exception("Missing buttons for ButtonsManager (must have at least 2 buttons !)");
        }

        m_Buttons[m_CurrentIdx].IsActive = true;
    }

    private void OnDestroy()
    {
        if (m_GestureRecognizer != null)
        {
            m_GestureRecognizer.Tapped -= OnTapped;
            m_GestureRecognizer.StopCapturingGestures();
            m_GestureRecognizer.Dispose();
        }
    }

    private void OnTapped(TappedEventArgs tappedEventArgs)
    {        
        // https://docs.unity3d.com/2018.2/Documentation/Manual/SpatialMappingCollider.html
        var gazeRay = new Ray(tappedEventArgs.headPose.position, tappedEventArgs.headPose.forward);
        var hits = Physics.RaycastAll(gazeRay, float.MaxValue);

        foreach (var hit in hits)
        {
            var targetObject = hit.collider.gameObject;
            Debug.Log($"Hit Object **\"**{targetObject}**\"** at position **\"**{hit.point}**\"**");
            
            var button = targetObject.GetComponent<Button>();
            if (button != null && button.IsActive)
            {
                button.IsActive = false;
                if (m_CurrentIdx < NumberOfButtons)
                {
                    var nextButton = GetNextButton();
                    nextButton.IsActive = true;
                } else {
                    // TODO: another class handling the timer
                    Debug.Log("Game finished");
                }
            }
        }
    }

    private Button GetNextButton() => m_Buttons[m_CurrentIdx++];
    
    public double GetDistance()
    {
        var totalDist = 0.0;
        for(var i = 1; i < m_Buttons.Count; i++)
        {
            totalDist += Vector3.Distance(m_Buttons[i - 1].transform.position, m_Buttons[i].transform.position);
        }
        return totalDist;
    }
    #endregion
}