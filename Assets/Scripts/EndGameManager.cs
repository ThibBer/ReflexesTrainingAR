using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : BaseGesture
{
    #region Fields
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text highestScore;
    [SerializeField]
    private Text highestScoreMessage;
    #endregion

    #region Methods
    // Start is called before the first frame update
    private void Start()
    {

        if (GameManager.isHighestScore) // Display new best score message
        {
            highestScoreMessage.gameObject.SetActive(true);
        }
        else // Display current highest score
        {
            highestScore.gameObject.SetActive(true);
            highestScore.text = highestScore.text + GameManager.highestScore;
        }
        // Display current score
        score.text = score.text + GameManager.Score;
    }
    #endregion
}
