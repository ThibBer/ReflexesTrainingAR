using System;
using UnityEngine;

[Serializable]
public struct Score
{
    // Using private fields instead of public properties since JsonUtility can't serialize properties
    [SerializeField]
    private int score;
    [SerializeField]
    private long timestamp;

    public Score(int score, long timestamp)
    {
        // JsonUtility can't serialize DateTime
        this.score = score;
        this.timestamp = timestamp;
    }

    public int GetScore() => score;

    public long GetTimestamp() => timestamp;
}
