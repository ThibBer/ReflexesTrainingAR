using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using System.Collections;

public class ButtonsManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject _buttonPrefab;

    private GestureRecognizer m_GestureRecognizer;
    private List<GameObject> m_Buttons;
    private int numberOfButtons = 15; // TODO : get this value from start menu
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

    private void Start()
    {
        spawnButtons();
        StartCoroutine(startGameRoutine());
    }

    IEnumerator startGameRoutine()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            m_Buttons[i].GetComponent<Button>().SetVisibility(true);
            yield return new WaitForSeconds(30);
            m_Buttons[i].GetComponent<Button>().SetVisibility(false);
        }
    }
        

    private void spawnButtons()
    {
        m_Buttons = new List<GameObject>();
        for (int i = 0; i < numberOfButtons; i++)
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;
            
            GameObject button = Instantiate(_buttonPrefab, new Vector3(UnityEngine.Random.Range(x+9f, x-9f), UnityEngine.Random.Range(y-5f, y+5f), 30), Quaternion.identity);
            m_Buttons.Add(button);
            button.transform.SetParent(this.transform, false);          
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
            targetObject.GetComponent<Button>().SetVisibility(false);
        }
    }

    #endregion
}