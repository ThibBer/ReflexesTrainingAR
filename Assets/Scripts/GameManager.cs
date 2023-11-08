﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : BaseGesture
{
    #region Fields
    [SerializeField]
    private Text tappedText;

    [SerializeField]
    private ButtonsManager buttonsManager;

    [SerializeField]
    private GameTimer gameTimer;

    public static int Score { get; private set; }
    #endregion

    #region Methods
    public override void handleHit(RaycastHit hit)
    {
        var targetObject = hit.collider.gameObject;

        var button = targetObject.GetComponent<Button>();
        if (button != null && button.IsActive)
        {
            button.IsActive = false;
            buttonsManager.GenerateNextButton();
            SetTappedText();
        }
    }

    private void SetTappedText()
    {
        tappedText.text = $"Tapped: {buttonsManager.Tapped}";
    }

    public void OnEnd()
    {
        buttonsManager.RemoveLast();
        Score = (int) Math.Round(buttonsManager.Tapped * buttonsManager.TotalDistance / GameTimer.Time);
        SceneManager.LoadScene(2);
    }
    #endregion
}
