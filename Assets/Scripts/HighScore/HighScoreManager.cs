using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private Scores m_HighScores;

    // Returns a shallow copy for encapsulation purposes
    public Scores HighScores => m_HighScores.GetCopy();

    private IFileHandlerStrategy<Scores> m_ScoreSaver;

    private void Start()
    {
        // Cannot be a static readonly member
        var hsFilePath = $@"{Application.persistentDataPath}/high_scores.json";
        m_HighScores = new Scores();
        m_ScoreSaver = new JsonFileHandler<Scores>(hsFilePath);
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        m_HighScores = m_ScoreSaver.ReadData() ?? new Scores();
    }

    private void SaveHighScores()
    {
        m_ScoreSaver.SaveData(m_HighScores);
    }

    public void ResetHighScores()
    {
        m_ScoreSaver.SaveData(new Scores());
    }

    public void AddScore(Score score)
    {
        m_HighScores.Add(score);
        SaveHighScores();
    }

    public int GetHighestScore()
    {
        return m_HighScores.GetHighestScore();
    }
}
