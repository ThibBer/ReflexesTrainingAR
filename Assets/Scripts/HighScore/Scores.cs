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
        // We avoid sorting/replacing the list if < MaxSize
        if (m_HighScores.Count >= MaxSize)
        {
            KeepLastScores();
        }
        m_HighScores.Add(score);
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
        // Can't save the best scores only (for the future chart displaying the score over time)
        m_HighScores = m_HighScores
            .OrderByDescending(s => s.ScoreDateTime)
            .Take(MaxSize-1) // n-1 last scores
            .ToList();
    }

    public int GetHighestScore()
    {
        var highestScore = m_HighScores.OrderByDescending(s => s.GetScore()).FirstOrDefault();

        if (highestScore.Equals(default(Score)))
        {
            return 0;
        }
        return highestScore.GetScore();
    }
}
