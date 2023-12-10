using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighScoresTab : MonoBehaviour
{
    private const float EntryHeight = 25f;
    /// <summary>
    /// The max amount of high scores to save and load
    /// </summary>
    public const int MaxHighScores = 10;

    [SerializeField]
    private HighScoreManager highScoreManager;

    void Start()
    {
        var highScores = highScoreManager.HighScores;
        var entries = transform.Find("Entries");
        var template = entries.Find("EntryTemplate");
        template.gameObject.SetActive(false);

        FillTheTable(highScores, entries, template);
    }

    private void FillTheTable(Scores highScores, Transform entries, Transform template)
    {
        var rank = 0;
        highScores
            .OrderByDescending(hs => hs.GetScore())
            .ThenByDescending(hs => hs.GetTimestamp())
            .Take(MaxHighScores)
            .ToList()
            .ForEach(score => 
            {
                rank += 1;
                var entryTransform = Instantiate(template, entries);
                var entryRectTransform = entryTransform.GetComponent<RectTransform>();

                entryRectTransform.anchoredPosition = new Vector2(0, -EntryHeight * (rank-1));
                entryTransform.gameObject.SetActive(true);

                entryTransform.Find("PosEntry").GetComponent<Text>().text = rank.ToString();
                entryTransform.Find("ScoreEntry").GetComponent<Text>().text = score.GetScore().ToString();
                entryTransform.Find("DateEntry").GetComponent<Text>().text = DisplayDate(score.GetTimestamp());
            });
    }

    private string DisplayDate(long timestamp) => DateUtils.TimestampToFormattedTime(timestamp, "MMMM dd yyyy - H:mm:ss");
}
