using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresTab : MonoBehaviour
{
    private const float EntryHeight = 35f;

    [SerializeField]
    private HighScoreManager highScoreManager;

    void Start()
    {
        var highScores = highScoreManager.HighScores;
        Debug.Log($"High scores are {highScores}");
        var entries = transform.Find("Entries");
        var template = entries.Find("EntryTemplate");
        template.gameObject.SetActive(false);

        FillTheTable(highScores, entries, template);
    }

    private void FillTheTable(HighScores highScores, Transform entries, Transform template)
    {
        for (var i = 0; i < highScores.Count(); i++)
        {
            var rank = i + 1;
            var entryTransform = Instantiate(template, entries);
            var entryRectTransform = entryTransform.GetComponent<RectTransform>();

            entryRectTransform.anchoredPosition = new Vector2(0, -EntryHeight * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("PosEntry").GetComponent<Text>().text = rank.ToString();
            entryTransform.Find("ScoreEntry").GetComponent<Text>().text = highScores[i].GetScore().ToString();
            entryTransform.Find("DateEntry").GetComponent<Text>().text = highScores[i].GetScoreDate();
        }
    }
}
