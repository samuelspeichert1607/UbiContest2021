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
    [SerializeField] private FadableScreen blackScreen;
    private CustomController _playerController;
    private RobotVoiceController _robotVoiceController;
    private static float _timeLeft;
    private static float _previousTimeLeft;
    private static int _previousPlayerCount;

    private bool isTimerOver = false;
    // private TextMeshProUGUI timerTextBox;

    void Start()
    {
        
        // Big problème : le timer se reset à chaque entrée d'un deuxième joueur
        _timeLeft = timeLimit;
        _previousTimeLeft = timeLimit;
        _robotVoiceController = GameObject.FindWithTag("RobotVoice").GetComponent<RobotVoiceController>();
        blackScreen.SetAlphaToZero();
        _playerController = GetComponentInParent<CustomController>();
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

        CheckTimer();
        UpdateOxygenBar();
        _previousPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    private void CheckTimer()
    {
        if (_timeLeft < 0 && !isTimerOver)
        {
            TimerIsOut();
        }
    }
    private void UpdateOxygenBar()
    {
        // At 15 minutes of time limit we don't notice, but the transition is rather rough
        // Could possibly be smoother
        float timePercentage = _timeLeft / timeLimit;
        oxygenFill.fillAmount = timePercentage;
        
    }
    private void TimerIsOut()
    {
        isTimerOver = true;
        _robotVoiceController.PlayLost();
        _playerController.disableMovement();
        blackScreen.SetFadingTime(2f * delayBeforeGameEnd/ 3f);
        blackScreen.FadeToFullAlpha();
        Invoke(nameof(GameIsLost), delayBeforeGameEnd);
    }


    public void CallGameSucceededAfterDefaultDelay()
    {
        _robotVoiceController.PlayWin();
        blackScreen.SetFadingTime(2f * delayBeforeGameEnd / 3f);
        blackScreen.FadeToFullAlpha();
        Invoke(nameof(GameIsWon), delayBeforeGameEnd);
    }

    private void GameIsWon()
    {
        //TODO disconnect properly player
        SceneManager.LoadScene("endingScreenSuccess");
    }
    
    private void GameIsLost()
    {
        //TODO disconnect properly player
        SceneManager.LoadScene("endingScreenFailure");
    }
}
