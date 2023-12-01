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
        SceneManager.LoadScene(4);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ScoresMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void About1()
    {
        SceneManager.LoadScene(5);
    }

    public void About2()
    {
        SceneManager.LoadScene(6);
    }

    public void About3()
    {
        SceneManager.LoadScene(7);
    }

    public void About4()
    {
        SceneManager.LoadScene(8);
    }

    public void About5()
    {
        SceneManager.LoadScene(9);
    }
    #endregion
}