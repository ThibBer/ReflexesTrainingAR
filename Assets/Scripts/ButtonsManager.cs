using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Random = System.Random;

public class ButtonsManager : MonoBehaviour
{
    #region Fields

    private GestureRecognizer m_GestureRecognizer;
    private List<Button> m_Buttons;
    private Random m_Random;

    #endregion

    #region Constructors

    #endregion

    #region Properties

    #endregion

    #region Methods

    private void Awake()
    {
        m_Random = new Random();
        
        m_GestureRecognizer = new GestureRecognizer();
        m_GestureRecognizer.StartCapturingGestures();
        m_GestureRecognizer.Tapped += OnTapped;
    }

    private void Start()
    {
        m_Buttons = FindObjectsOfType<Button>().ToList();
        Debug.Log("Buttons count : " + m_Buttons.Count);

        if (m_Buttons.Count <= 1)
        {
            throw new Exception("Missing buttons for ButtonsManager (must have at least 2 buttons !)");
        }

        m_Buttons[m_Random.Next(0, m_Buttons.Count - 1)].IsActive = true;
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
        Debug.Log("OnTapped");
        
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
                
                var nextButton = GetNextButton(button);
                nextButton.IsActive = true;
            }
        }
    }

    private Button GetNextButton(Button clickedButton)
    {
        var iClickedButton = m_Buttons.IndexOf(clickedButton);
        var iRandomButton = m_Random.Next(0, m_Buttons.Count);

        while (iRandomButton == iClickedButton)
        {
            iRandomButton = m_Random.Next(0, m_Buttons.Count - 1);
        }

        return m_Buttons[iRandomButton];
    }
    
    #endregion
}