using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : BaseGesture
{
    #region Fields
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject configButton;

    #endregion

    #region Methods
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public override void handleHit(RaycastHit hit)
    {
        GameObject targetObject = hit.collider.gameObject;
        if (ReferenceEquals(startButton, targetObject))
        {
            SceneManager.LoadScene(1);
        }
        else if (ReferenceEquals(configButton, targetObject))
        {
            SceneManager.LoadScene(3);
        }
    }

    // Maybe to remove
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void configGame()
    {
        SceneManager.LoadScene(3);
    }

    #endregion
}