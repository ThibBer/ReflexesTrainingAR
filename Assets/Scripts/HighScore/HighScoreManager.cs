using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private HighScoreTab m_HighScoresTab;
    private IFileHandlerStrategy<HighScoreTab> m_ScoreSaver;

    private void Start()
    {
        // Cannot be a static readonly member
        var hsFilePath = $@"{Application.persistentDataPath}/high_scores.json";
        Debug.Log($"Persistent data path file: {hsFilePath}");
        m_HighScoresTab = new HighScoreTab();
        m_ScoreSaver = new JsonFileHandler<HighScoreTab>(hsFilePath);
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        m_HighScoresTab = m_ScoreSaver.ReadData() ?? new HighScoreTab();
        m_HighScoresTab.OrderScores();
        Debug.Log($"High scores loaded: {m_HighScoresTab}");
    }

    private void SaveHighScores()
    {
        m_HighScoresTab.OrderScores();
        m_ScoreSaver.SaveData(m_HighScoresTab);
    }

    private void ResetHighScores()
    {
        m_ScoreSaver.SaveData(new HighScoreTab());
    }

    public void AddScore(HighScore score)
    {
        m_HighScoresTab.Add(score);
        SaveHighScores();
    }
}
