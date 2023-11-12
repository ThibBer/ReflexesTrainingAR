using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using System.Text;

[Serializable]
public class HighScores
{
    /// <summary>
    /// The max amount of high scores to save and load
    /// </summary>
    public const int MaxHighScores = 10;

    [SerializeField]
    private List<HighScore> m_HighScores;

    public HighScore this[int i]
    {
        get { return m_HighScores[i]; }
    }

    public HighScores()
    {
        m_HighScores = new List<HighScore>();
    }

    public void Add(HighScore score)
    {
        m_HighScores.Add(score);
        OrderScores();
    }

    public HighScores GetCopy()
    {
        var newHighScores = new HighScores();
        m_HighScores.ForEach(s => newHighScores.Add(s));
        return newHighScores;
    }

    public int Count() => m_HighScores.Count();

    public void OrderScores()
    {
        Debug.Log($"Ordering: {m_HighScores}");
        m_HighScores = m_HighScores.OrderByDescending(hs => hs.GetScore())
            .ThenBy(hs => hs.GetScoreDate())
            .Take(MaxHighScores)
            .ToList();
    }
}
