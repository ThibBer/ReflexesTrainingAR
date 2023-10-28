using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // TODO: get from difficulty level
    private const int Time = 60;

    [SerializeField]
    private Image timerFiller;

    [SerializeField]
    private Text timerText;

    private int m_TimeRemaining;

    // Start is called before the first frame update
    private void Start()
    {
        m_TimeRemaining = Time;
        StartCoroutine(UpdateGameTimer());
    }

    private IEnumerator UpdateGameTimer()
    {
        while(m_TimeRemaining >= 0)
        {
            timerFiller.fillAmount = Mathf.InverseLerp(0, Time, m_TimeRemaining);
            timerText.text = $"{m_TimeRemaining / 60:00}:{m_TimeRemaining % 60:00}";
            m_TimeRemaining--;
            yield return new WaitForSeconds(1.0f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        // TODO
    }
}
