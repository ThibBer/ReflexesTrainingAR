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
        score.text = score.text + GameManager.Score;
        Vector3 pos = Camera.main.transform.position;
        restartButton = Instantiate(restartButton, new Vector3(pos.x, pos.y - 3, pos.z + 100), restartButton.transform.rotation);
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
