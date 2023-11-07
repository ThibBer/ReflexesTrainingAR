using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Image timerFiller;

    [SerializeField]
    private Text timerText;

    private int m_TimeRemaining;
    #endregion

    #region Events
    public UnityEvent onTimerEnd;
    #endregion

    #region Methods
    private void Start()
    {
        m_TimeRemaining = ConfigurationManager.getInstance().getGameTime_seconds();
        StartCoroutine(UpdateGameTimer());
    }

    private IEnumerator UpdateGameTimer()
    {
        while(m_TimeRemaining >= 0)
        {
            timerFiller.fillAmount = Mathf.InverseLerp(0, ConfigurationManager.getInstance().getGameTime_seconds(), m_TimeRemaining);
            timerText.text = $"{m_TimeRemaining / 60:00}:{m_TimeRemaining % 60:00}";
            m_TimeRemaining--;
            yield return new WaitForSeconds(1.0f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        onTimerEnd?.Invoke();
    }
    #endregion
}
