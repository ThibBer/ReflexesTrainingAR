using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;

public class MenuManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject startButton;

    private GestureRecognizer m_GestureRecognizer;
    #endregion
    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        var x = Camera.main.transform.position.x;
        var y = Camera.main.transform.position.y;
        var z = Camera.main.transform.position.z;
        var btn = Instantiate(startButton, new Vector3(x, y-3, z + 100), startButton.transform.rotation);
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