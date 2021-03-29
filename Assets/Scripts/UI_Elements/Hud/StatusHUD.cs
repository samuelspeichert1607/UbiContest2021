using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    [SerializeField] private float delayBeforeGameEnd = 15f;
    [SerializeField] private Image oxygenFill;
    private static float _timeLeft;
    private static float _previousTimeLeft;
    private static int _previousPlayerCount;
    // private TextMeshProUGUI timerTextBox;

    void Start()
    {
        
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
            // timerTextBox.text = $"{minutes}:" + (seconds > 9 ? seconds.ToString() : "0" + seconds.ToString());
        }
        else
        {
            Invoke(nameof(GameIsLost), delayBeforeGameEnd);
        }
    }

    private void GameIsLost()
    {
        SceneManager.LoadScene("endingScreenFailure");
    }

    private void UpdateOxygenBar()
    {
        // At 15 minutes of time limit we don't notice, but the transition is rather rough
        // Could possibly be smoother
        float timePercentage = _timeLeft / timeLimit;
        oxygenFill.fillAmount = timePercentage;
        
    }
}
