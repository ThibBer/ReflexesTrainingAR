using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private HighScores m_HighScores;

    public HighScores HighScores
    {
        get => m_HighScores.GetCopy();
    }

    private IFileHandlerStrategy<HighScores> m_ScoreSaver;

    private void Start()
    {
        // Cannot be a static readonly member
        var hsFilePath = $@"{Application.persistentDataPath}/high_scores.json";
        Debug.Log($"Persistent data path file: {hsFilePath}");
        m_HighScores = new HighScores();
        m_ScoreSaver = new JsonFileHandler<HighScores>(hsFilePath);
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        m_HighScores = m_ScoreSaver.ReadData() ?? new HighScores();
        m_HighScores.OrderScores();
        Debug.Log($"High scores loaded: {m_HighScores}");
    }

    private void SaveHighScores()
    {
        m_HighScores.OrderScores();
        m_ScoreSaver.SaveData(m_HighScores);
    }

    public void ResetHighScores()
    {
        m_ScoreSaver.SaveData(new HighScores());
    }

    public void AddScore(HighScore score)
    {
        m_HighScores.Add(score);
        SaveHighScores();
    }
}
