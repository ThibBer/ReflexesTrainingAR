using UnityEngine;
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

    [SerializeField]
    private HighScoreManager highScoreManager;

    [SerializeField] 
    private GameObject directionalArrow;

    private Button m_LastGeneratedButton;
    #endregion

    #region Properties
    
    public static int Score { get; private set; }

    public static int HighestScore { get; private set; }

    public static bool IsHighestScore { get; private set; }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        buttonsManager.ButtonGenerated += OnButtonGenerated;
    }

    private void OnButtonGenerated(object sender, Button button)
    {
        m_LastGeneratedButton = button;
    }

    public override void handleHit(RaycastHit hit)
    {
        var targetObject = hit.collider.gameObject;

        var button = targetObject.GetComponent<Button>();
        if (button != null && button.IsActive)
        {
            button.PlaySound();
            button.IsActive = false;
            
            buttonsManager.GenerateNextButton();
            SetTappedText();
        }
    }

    private void SetTappedText()
    {
        tappedText.text = $"Boutons: {buttonsManager.Tapped}";
    }

    public void OnEnd()
    {
        buttonsManager.RemoveLast();
        Score = Mathf.RoundToInt(buttonsManager.Tapped * buttonsManager.TotalDistance / ConfigurationDatabase.Instance.GameTimeSeconds);
        HighestScore = highScoreManager.GetHighestScore();
        IsHighestScore = Score > HighestScore;
        highScoreManager.AddScore(new Score(Score, DateTime.Now));
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if (m_LastGeneratedButton != null)
        {
            directionalArrow.transform.LookAt(m_LastGeneratedButton.transform);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        buttonsManager.ButtonGenerated -= OnButtonGenerated;
    }

    #endregion
}
