using System;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Button : MonoBehaviour
{
    public Color defaultColor;
    public Color activeColor;
    
    private GestureRecognizer m_GestureRecognizer;
    private bool m_IsActive;

    private void Awake()
    {
        m_GestureRecognizer = new GestureRecognizer();
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetColor(defaultColor);
        
        m_GestureRecognizer.Tapped += OnTapped;
        m_GestureRecognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        m_GestureRecognizer.StopCapturingGestures();
        m_GestureRecognizer.Tapped -= OnTapped;
        m_GestureRecognizer.Dispose();
    }

    private void OnTapped(TappedEventArgs tappedEventArgs)
    {
        Console.WriteLine($"Tap on {name}");
        Console.WriteLine($"TapCount = {tappedEventArgs.tapCount}");
        Console.WriteLine($"Source = {tappedEventArgs.source}");
        Console.WriteLine($"SourcePose = {tappedEventArgs.sourcePose}");
        Console.WriteLine($"HeadPose = {tappedEventArgs.headPose}");

        IsActive = false;
    }

    private void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public bool IsActive
    {
        get => m_IsActive;
        set
        {
            if (m_IsActive != value)
            {
                m_IsActive = value;
                
                if (m_IsActive)
                {
                    SetColor(activeColor);
                    m_GestureRecognizer.StartCapturingGestures();
                }
                else
                {
                    SetColor(defaultColor);
                    m_GestureRecognizer.StopCapturingGestures();
                }
            }
        }
    }
}
