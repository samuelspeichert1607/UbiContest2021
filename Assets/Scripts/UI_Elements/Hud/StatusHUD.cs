using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    [SerializeField]
    private float timeLimit;
    private static float _timeLeft;
    private static float _previousTimeLeft;
    private static int _previousPlayerCount;
    private Image oxygenFill;
    private TextMeshProUGUI timerTextBox;

    void Start()
    {
        oxygenFill = transform.GetChild(3).transform.GetComponent<Image>();
        timerTextBox = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        // Big problème : le timer se reset à chaque entrée d'un deuxième joueur
        _timeLeft = timeLimit;
        _previousTimeLeft = timeLimit;
    }

    void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > _previousPlayerCount)
            {
                _timeLeft = _previousTimeLeft;
            }
            // Pourrait se changer pour diviser par le nombre de joueurs
            _timeLeft -= Time.deltaTime / 2;
        }
        else if(PhotonNetwork.CurrentRoom.PlayerCount < _previousPlayerCount)
        {
            _previousTimeLeft = _timeLeft;
        }

        UpdateTimerText();
        UpdateOxygenBar();
        _previousPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    private void UpdateTimerText()
    {
        if (_timeLeft >= 0)
        {
            int minutes = Mathf.FloorToInt(_timeLeft / 60);
            int seconds = Mathf.FloorToInt(_timeLeft % 60);
            timerTextBox.text = $"{minutes}:" + (seconds > 9 ? seconds.ToString() : "0" + seconds.ToString());
        }
        else
        {
            // To keep the timer from going to a negative value that would make the game crash
            timerTextBox.text = "0:00";
            _timeLeft = -1;
        }
    }

    private void UpdateOxygenBar()
    {
        // At 15 minutes of time limit we don't notice, but the transition is rather rough
        // Could possibly be smoother
        float timePercentage = _timeLeft / timeLimit;
        oxygenFill.fillAmount = timePercentage;
        
    }
}
