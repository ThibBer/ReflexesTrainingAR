using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Text tappedText;

    [SerializeField]
    private ButtonsManager buttonsManager;

    [SerializeField]
    private GameTimer gameTimer;

    private GestureRecognizer m_GestureRecognizer;

    public static int score;
    #endregion

    #region Methods
    private void Start()
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
            var targetObject = hit.collider.gameObject;

            var button = targetObject.GetComponent<Button>();
            if (button != null && button.IsActive)
            {
                button.IsActive = false;
                buttonsManager.GenerateNextButton();
                SetTappedText();
            }
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

    private void SetTappedText()
    {
        tappedText.text = $"Tapped: {buttonsManager.GeneratedNumber - 1}";
    }

    public void OnEnd()
    {
        // TODO: display the score, play again, ... (menus)
        buttonsManager.RemoveLast();
        score = buttonsManager.GeneratedNumber;
        Debug.Log("Game finished");
        SceneManager.LoadScene(2);
    }
    #endregion
}
