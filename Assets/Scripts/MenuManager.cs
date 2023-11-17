using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : BaseGesture
{
    #region Fields
    #endregion

    #region Methods
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ConfigGame()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}