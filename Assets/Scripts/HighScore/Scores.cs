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

    public Score this[int i]
    {
        get { return m_HighScores[i]; }
    }

    public Scores()
    {
        m_HighScores = new List<Score>();
    }

    private Scores(List<Score> scores)
    {
        m_HighScores = scores;
    }

    public void Add(Score score)
    {
        m_HighScores.Add(score);
    }

    public Scores GetCopy()
    {
        var newHighScores = new Scores();
        m_HighScores.ForEach(s => newHighScores.Add(s));
        return newHighScores;
    }

    /*public void OrderByDate()
    {
        Debug.Log($"Ordering: {m_HighScores}");
        m_HighScores = m_HighScores
            .OrderByDescending(hs => hs.GetScore())
            .ThenBy(hs => hs.ScoreDateTime)
            .ToList();
    }

    public void OrderByScore()
    {
        m_HighScores = m_HighScores
            .OrderByDescending(hs => hs.GetScore())
            .ThenBy(hs => hs.ScoreDateTime)
            .ToList();
    }*/

    public IEnumerator<Score> GetEnumerator()
    {
        return m_HighScores.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
