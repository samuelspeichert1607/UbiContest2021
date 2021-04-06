using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    [SerializeField] private float delayBeforeGameEnd = 15f;
    [SerializeField] private Image oxygenFill;
    [SerializeField] private FadableScreen blackScreen;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject deathCam;

    private Vignette _playerCameraPostProcessVignette;
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
        playerCamera.GetComponent<PostProcessVolume>().profile.TryGetSettings(out _playerCameraPostProcessVignette);
    }

    void Update()
    {
        // if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        // {
            // if (PhotonNetwork.CurrentRoom.PlayerCount > _previousPlayerCount)
            // {
                _timeLeft = _previousTimeLeft;
            // }
            // Pourrait se changer pour diviser par le nombre de joueurs
            _timeLeft -= Time.deltaTime / 2;
        // }
        // else if(PhotonNetwork.CurrentRoom.PlayerCount < _previousPlayerCount)
        // {
            _previousTimeLeft = _timeLeft;
        // }

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

        PlayerDeathInitialPhase();
        
        Invoke(nameof(BlackscreenFinalFading), 1f);
        Invoke(nameof(GameIsLost), delayBeforeGameEnd);
    }
    
    private void PlayerDeathInitialPhase()
    {
        _playerController.disableMovement();

        float shortBlackoutTime = 0.3f;
        // blackScreen.SetFadingTime(shortBlackoutTime/ 2f);
        // blackScreen.FadeToFullAlpha();
        StartCoroutine(FadeIntensityUpToValue(_playerCameraPostProcessVignette, 0.7f));
        
        Invoke(nameof(PlayDeathAnimation), shortBlackoutTime);
    }

    private void PlayDeathAnimation()
    {
        _playerController.PlayDeathAnimation();
        playerCamera.SetActive(false);
        deathCam.SetActive(true);
        
        // blackScreen.FadeToZeroAlpha();
    }
    
    private void BlackscreenFinalFading()
    {
        // blackScreen.SetFadingTime(2f * delayBeforeGameEnd/ 3f);
        // blackScreen.FadeToFullAlpha();
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
        // 4 = endingScreenSuccess
        GetComponentInParent<PlayerController>().Disconnect(4);
    }
    
    private void GameIsLost()
    {
        // 5 = endingScreenFailure
        GetComponentInParent<PlayerController>().Disconnect(5);
    }
    
    IEnumerator FadeIntensityUpToValue (Vignette vignette, float intensityValue)
    {
        while (vignette.intensity < intensityValue)
        {
            vignette.intensity.value += Time.deltaTime / 2f;
            Debug.Log(vignette.intensity.value);
            yield return null;
        }
    }

    IEnumerator FadeIntensityDownToValue (Vignette vignette, float intensityValue)
    {
        while (vignette.intensity > intensityValue) {
            vignette.intensity.value += Time.deltaTime / 2f;
            yield return null;
        }
    }

}
