using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : BaseGesture
{
    #region Fields
    [SerializeField]
    private GameObject startButton;

    #endregion

    #region Methods
    // Start is called before the first frame update
    private void Start()
    {
        Vector3 pos = Camera.main.transform.position;
        startButton = Instantiate(startButton, new Vector3(pos.x, pos.y-3, pos.z + 100), startButton.transform.rotation);
    }

    public override void handleHit(RaycastHit hit)
    {
        GameObject targetObject = hit.collider.gameObject;
        if (ReferenceEquals(startButton, targetObject))
        {
            SceneManager.LoadScene(1);
        }
    }
    #endregion
}