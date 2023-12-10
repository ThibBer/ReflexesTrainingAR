using System;
using UnityEngine;

[Serializable]
public struct Score
{
    // Using private fields instead of public properties since JsonUtility can't serialize properties
    [SerializeField]
    private int score;
    //[SerializeField]
    //private string scoreDate;
    [SerializeField]
    private long timestamp;

    // Needed to order the scores by date for the chart
    //public DateTime ScoreDateTime { get; }

    public Score(int score, long timestamp)
    {
        this.score = score;
        this.timestamp = timestamp;
        //ScoreDateTime = scoreDate;
        // JsonUtility can't serialize DateTime
        // Format exemple: November 12 2023 - 16:50:30
        //this.scoreDate = scoreDate.ToString("MMMM dd yyyy - H:mm:ss");
    }

    public int GetScore() => score;

    public long GetTimestamp() => timestamp;

    //public string GetScoreDate() => scoreDate;
}
