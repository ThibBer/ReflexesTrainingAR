using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : BaseGesture
{
    #region Fields
    #endregion

    #region Methods
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void configGame()
    {
        SceneManager.LoadScene(3);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}