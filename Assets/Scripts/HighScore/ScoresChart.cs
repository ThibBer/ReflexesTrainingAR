using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoresChart : MonoBehaviour
{
    [SerializeField]
    private Sprite chartCircle;

    [SerializeField]
    private HighScoreManager highScoreManager;

    private RectTransform chartContainer;

    void Start()
    {
        chartContainer = transform.Find("ChartContainer").GetComponent<RectTransform>();
        // Fake scores for testing purpose
        var fakeScores = new Scores();

        fakeScores.Add(new Score(12, DateTime.Now.AddHours(2)));
        fakeScores.Add(new Score(4, DateTime.Now.AddDays(2)));
        fakeScores.Add(new Score(6, DateTime.Now));
        fakeScores.Add(new Score(2, DateTime.Now));
        fakeScores.Add(new Score(100, DateTime.Now.AddMinutes(5)));
        fakeScores.Add(new Score(250, DateTime.Now.AddMinutes(5)));
        fakeScores.Add(new Score(25, DateTime.Now));
        fakeScores.Add(new Score(30, DateTime.Now));
        fakeScores.Add(new Score(50, DateTime.Now));
        fakeScores.Add(new Score(25, DateTime.Now));

        //DisplayChart(highScoreManager.HighScores);
        DisplayChart(fakeScores);
    }

    private GameObject CreateChartCircle(Vector2 position)
    {
        var circleObject = new GameObject("Circle", typeof(Image));
        circleObject.transform.SetParent(chartContainer, false);
        circleObject.GetComponent<Image>().sprite = chartCircle;
        var rectTransform = circleObject.GetComponent<RectTransform>();

        SetRectTransformAnchor(rectTransform, new Vector2(0, 0), new Vector2(0, 0), position);
        rectTransform.sizeDelta = new Vector2(11, 11);

        return circleObject;
    }

    private void DisplayChart(Scores scores)
    {
        var height = chartContainer.sizeDelta.y;
        var maxHeight = 250f;
        var width = 52f;

        GameObject lastCircleObject = null;
        for(var i = 0; i < scores.Count; i++)
        {
            var xPos = i * width;
            var yPos = (scores[i].GetScore() / maxHeight) * height;
            var circleObject = CreateChartCircle(new Vector2(xPos, yPos));

            if(lastCircleObject != null)
            {
                ConnectCircles(
                    lastCircleObject.GetComponent<RectTransform>().anchoredPosition, 
                    circleObject.GetComponent<RectTransform>().anchoredPosition
                );
            }
            lastCircleObject = circleObject;
        }
    }

    private void ConnectCircles(Vector2 firstCirclePos, Vector2 secondCirclePos)
    {
        var lineObject = new GameObject("Line", typeof(Image));
        lineObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.75f);
        lineObject.transform.SetParent(chartContainer, false);
        var rectTransform = lineObject.GetComponent<RectTransform>();
        var direction = (secondCirclePos - firstCirclePos).normalized;
        var distance = Vector2.Distance(secondCirclePos, firstCirclePos);

        var anchoredPosition = firstCirclePos + direction * distance * 0.5f;
        SetRectTransformAnchor(rectTransform, new Vector2(0, 0), new Vector2(0, 0), anchoredPosition);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, direction));
    }

    private void SetRectTransformAnchor(RectTransform transform, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition)
    {
        transform.anchorMin = anchorMin;
        transform.anchorMax = anchorMax;
        transform.anchoredPosition = anchoredPosition;
    }
}
