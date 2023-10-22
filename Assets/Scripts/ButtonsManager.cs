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
     * Use a collection and generate all buttons first if more convenient
     */
    private Button m_CurrentButton;
    private int m_ButtonsLeft = NumberOfButtons;
    // Use a get/private set property later if needed
    private float m_TotalDistance;

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


    private void GenerateNextButton()
    {
        var x = Camera.main.transform.position.x;
        var y = Camera.main.transform.position.y;
        var btn = Instantiate(button, new Vector3(Random.Range(x - 5f, x + 5f), Random.Range(x - 5f, x + 5f), 20), button.transform.rotation); // TODO: define ranges

        if (m_CurrentButton == null)
        {
            m_CurrentButton = btn;
        }

        m_TotalDistance += m_CurrentButton != null ? Vector3.Distance(m_CurrentButton.transform.position, btn.transform.position) : 0;
        btn.IsActive = true;
        m_CurrentButton = btn;
        m_ButtonsLeft--;
    }

    private void Start()
    {
        GenerateNextButton();
        button.IsActive = false; // Template button: disable/hide it in another way

        if (NumberOfButtons < 2)
        {
            throw new Exception("Missing buttons for ButtonsManager (must have at least 2 buttons !)");
        }
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
                button.IsActive = false; // SetActive(false) over Destroy (see the differences in the documentation)
                if (m_ButtonsLeft > 0)
                {
                    GenerateNextButton();
                } else {
                    // TODO: another class handling the timer
                    Debug.Log("Game finished");
                }
            }
        }
    }
    #endregion
}