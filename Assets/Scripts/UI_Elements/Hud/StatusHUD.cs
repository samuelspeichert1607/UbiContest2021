using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    [SerializeField]
    private float timeLimit;
    private float timeLeft;
    private Image oxygenFill;
    private TextMeshProUGUI timerTextBox;

    void Start()
    {
        oxygenFill = transform.GetChild(3).transform.GetComponent<Image>();
        timeLeft = timeLimit;
        timerTextBox = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateTimerText();
        UpdateOxygenBar();
    }

    private void UpdateTimerText()
    {
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.CeilToInt(timeLeft % 60);
            string secondString = seconds > 9 ? seconds.ToString() : "0" + seconds.ToString();
            timerTextBox.text = $"{minutes}:" + secondString;
        }
        else
        {
            // To keep the timer from going to a negative value that would make the game crash
            timerTextBox.text = "0:00";
            timeLeft = -1;
        }
    }

    private void UpdateOxygenBar()
    {
        // At 15 minutes of time limit we don't notice, but the transition is rather rough
        // Could possibly be smoother
        float timePercentage = timeLeft / timeLimit;
        oxygenFill.fillAmount = timePercentage;
        
    }
}
