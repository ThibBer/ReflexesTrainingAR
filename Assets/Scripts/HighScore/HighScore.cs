using System;
using UnityEngine;

[Serializable]
public struct HighScore
{
    // Using private fields instead of public properties since JsonUtility can't serialize properties
    [SerializeField]
    private int score;
    [SerializeField]
    private string scoreDate;

    public HighScore(int score, DateTime scoreDate)
    {
        this.score = score;
        // JsonUtility can't serialize DateTime
        this.scoreDate = scoreDate.ToString();
    }

    public int GetScore() => score;

    public string GetScoreDate() => scoreDate;
}
