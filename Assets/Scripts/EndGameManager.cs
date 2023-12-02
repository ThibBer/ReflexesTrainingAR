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
        if (GameManager.isHighestScore)
        {
            highestScoreMessage.gameObject.SetActive(true);
            Debug.Log("endgamemanager: you got a new high score");
        }
        else
        {
            highestScore.gameObject.SetActive(true);
            highestScore.text = highestScore.text + GameManager.highestScore;
        }
        score.text = score.text + GameManager.Score;
    }
    #endregion
}
