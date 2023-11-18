using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : BaseGesture
{
    #region Fields
    [SerializeField]
    private Text score;

    [SerializeField]
    private GameObject restartButton;
    #endregion

    #region Methods
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log($"Score: {GameManager.Score}");
        Debug.Log($"Score text: {score.text}");
        score.text = score.text + GameManager.Score;
    }

    public override void handleHit(RaycastHit hit)
    {
        var targetObject = hit.collider.gameObject;
        if (GameObject.ReferenceEquals(restartButton, targetObject))
        {
            SceneManager.LoadScene(1);
        }
    }

    #endregion
}
