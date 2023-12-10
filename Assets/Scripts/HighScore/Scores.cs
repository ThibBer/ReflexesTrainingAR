using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[Serializable]
public class Scores : IEnumerable<Score>
{
    public const int MaxSize = 100;

    [SerializeField]
    private List<Score> m_HighScores;

    public int Count => m_HighScores.Count;

    public Score this[int i] => m_HighScores[i];

    public Scores()
    {
        m_HighScores = new List<Score>();
    }

    public void Add(Score score)
    {
        m_HighScores.Add(score);

        // We avoid sorting/replacing the list if < MaxSize
        if (m_HighScores.Count >= MaxSize)
        {
            KeepLastScores();
        }
    }

    public Scores GetCopy()
    {
        var newHighScores = new Scores();
        m_HighScores.ForEach(s => newHighScores.Add(s));
        return newHighScores;
    }

    public IEnumerator<Score> GetEnumerator() => m_HighScores.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void KeepLastScores()
    {
        // Keep the n latests scores only
        m_HighScores = m_HighScores
            .OrderByDescending(s => s.GetTimestamp())
            .Take(MaxSize) // n last scores
            .ToList();
    }

    public int GetHighestScore()
    {
        var highestScore = m_HighScores.OrderByDescending(s => s.GetScore()).FirstOrDefault();
        return highestScore.Equals(default(Score)) ? 0 : highestScore.GetScore();
    }
}
