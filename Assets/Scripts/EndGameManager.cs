using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;

    [SerializeField]
    private GameObject restartButton;
    private GestureRecognizer m_GestureRecognizer;
    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        _score.text = _score.text + GameManager.score.ToString();
        var x = Camera.main.transform.position.x;
        var y = Camera.main.transform.position.y;
        var z = Camera.main.transform.position.z;
        var btn = Instantiate(restartButton, new Vector3(x, y - 3, z + 100), restartButton.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        m_GestureRecognizer = new GestureRecognizer();
        m_GestureRecognizer.StartCapturingGestures();
        m_GestureRecognizer.Tapped += OnTapped;
    }

    private void OnTapped(TappedEventArgs tappedEventArgs)
    {
        // https://docs.unity3d.com/2018.2/Documentation/Manual/SpatialMappingCollider.html
        var gazeRay = new Ray(tappedEventArgs.headPose.position, tappedEventArgs.headPose.forward);
        var hits = Physics.RaycastAll(gazeRay, float.MaxValue);
        foreach (var hit in hits)
        {
            SceneManager.LoadScene(1);
        }
    }
    #endregion

}
