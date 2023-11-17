using UnityEngine;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private Scores m_HighScores;

    public Scores HighScores
    {
        // Returns a copy for encapsulation purposes
        get => m_HighScores.GetCopy();
    }

    private IFileHandlerStrategy<Scores> m_ScoreSaver;

    private void Start()
    {
        // Cannot be a static readonly member
        var hsFilePath = $@"{Application.persistentDataPath}/high_scores.json";
        Debug.Log($"Persistent data path file: {hsFilePath}");
        m_HighScores = new Scores();
        m_ScoreSaver = new JsonFileHandler<Scores>(hsFilePath);
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        m_HighScores = m_ScoreSaver.ReadData() ?? new Scores();
        Debug.Log($"High scores loaded: {m_HighScores}");
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

    
}
