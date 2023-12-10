using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class ScoresChart : MonoBehaviour
{
    private const float YStartPosition = -7.5f;
    private const float XStartPosition = -32f;
    private float yMaxValue = 150f;

    [SerializeField]
    private Sprite chartCircle;

    private RectTransform textTemplateX;
    private RectTransform textTemplateY;

    private RectTransform verticalDashTemplate;
    private RectTransform horizontalDashTemplate;

    [SerializeField]
    private HighScoreManager highScoreManager;

    private RectTransform chartContainer;

    private void Start()
    {
        FindComponents();
        
        // Take the 10 last scores and then order by oldest -> newest
        var scores = highScoreManager.HighScores
            .OrderByDescending(score => score.GetTimestamp())
            .Take(10)
            .Reverse()
            .ToArray();

        if(scores != null && scores.Length > 0)
        {
            yMaxValue = DefineYMax(scores.Max(s => s.GetScore()));
        }
        
        DisplayChart(scores);
    }

    private int DefineYMax(int maxScore) => maxScore > 0 ? maxScore + (maxScore / 10) : Mathf.FloorToInt(yMaxValue);
         
    private void FindComponents()
    {
        chartContainer = transform.Find("ChartContainer").GetComponent<RectTransform>();
        textTemplateX = chartContainer.Find("TextTemplateX").GetComponent<RectTransform>();
        textTemplateY = chartContainer.Find("TextTemplateY").GetComponent<RectTransform>();
        verticalDashTemplate = chartContainer.Find("VerticalDashTemplate").GetComponent<RectTransform>();
        horizontalDashTemplate = chartContainer.Find("HorizontalDashTemplate").GetComponent<RectTransform>();
    }

    private GameObject CreateChartCircle(Vector2 position)
    {
        var circleObject = new GameObject("Circle", typeof(Image));
        circleObject.transform.SetParent(chartContainer, false);
        circleObject.GetComponent<Image>().sprite = chartCircle;
        var rectTransform = circleObject.GetComponent<RectTransform>();

        SetRectTransformAnchor(rectTransform, new Vector2(0, 0), new Vector2(0, 0), position);
        rectTransform.sizeDelta = new Vector2(8, 8);

        return circleObject;
    }

    private void DisplayChart(Score[] scores)
    {
        var height = chartContainer.sizeDelta.y;
        var width = 50f;

        GameObject lastCircleObject = null;
        for(var i = 0; i < scores.Length; i++)
        {
            var xPos = i * width;
            var yPos = (scores[i].GetScore() / yMaxValue) * height;
            var circleObject = CreateChartCircle(new Vector2(xPos, yPos));

            if(lastCircleObject != null)
            {
                ConnectCircles(lastCircleObject.GetComponent<RectTransform>().anchoredPosition, 
                    circleObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleObject = circleObject;

            var labelTextX = Instantiate(textTemplateX);
            SetUpXLabelText(labelTextX, xPos, scores[i], i+1);
            CreateVerticalDashLine(xPos);
        }

        SetUpYAxis(height, yMaxValue);
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
        rectTransform.sizeDelta = new Vector2(distance, 2f);
        rectTransform.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, direction));
    }

    private void SetRectTransformAnchor(RectTransform transform, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition)
    {
        transform.anchorMin = anchorMin;
        transform.anchorMax = anchorMax;
        transform.anchoredPosition = anchoredPosition;
    }

    private void SetUpXLabelText(RectTransform labelTextX, float x, Score score, int gameNumber)
    {
        labelTextX.SetParent(chartContainer);
        labelTextX.gameObject.SetActive(true);
        labelTextX.anchoredPosition = new Vector2(x, YStartPosition);
        labelTextX.GetComponent<Text>().text = $"{DisplayDate(score.GetTimestamp())}";
    }

    private void SetUpYAxis(float chartHeight, float maxHeight)
    {
        const int SeparatorCount = 10;

        for (var i = 0; i < SeparatorCount; i++)
        {
            var normalizedValue = (float) i / SeparatorCount;

            var y = normalizedValue * chartHeight;
            var labelTextY = Instantiate(textTemplateY);
            labelTextY.SetParent(chartContainer);
            labelTextY.gameObject.SetActive(true);
            labelTextY.anchoredPosition = new Vector2(XStartPosition, y);
            labelTextY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * maxHeight).ToString();

            CreateHorizontalDashLine(normalizedValue * chartHeight);
        }
    }

    private void CreateVerticalDashLine(float x)
    {
        var gridDashX = Instantiate(verticalDashTemplate);
        gridDashX.SetParent(chartContainer, false);
        gridDashX.gameObject.SetActive(true);
        gridDashX.anchoredPosition = new Vector2(x, YStartPosition);
    }

    private void CreateHorizontalDashLine(float y)
    {
        var gridDashY = Instantiate(horizontalDashTemplate);
        gridDashY.SetParent(chartContainer, false);
        gridDashY.gameObject.SetActive(true);
        gridDashY.anchoredPosition = new Vector2(XStartPosition, y);
    }

    private string DisplayDate(long timestamp) => DateUtils.TimestampToFormattedTime(timestamp, "dd-MM");
}
