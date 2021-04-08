using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour, MusicPlayerListener
{
    [SerializeField] private float timeLimit;
    [SerializeField] private float timeBeforeOxygenStart;
    [SerializeField] private float delayBeforeGameEnd = 15f;
    [SerializeField] private Image oxygenFill;
    [SerializeField] private FadableScreen blackScreen;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject deathCam;
    [SerializeField] private float shortBlackoutTime = 0.5f;
    [SerializeField] private float longBlackoutTime = 15f;
    [SerializeField] private GameObject oxygenBar;

    [SerializeField] private AudioClip heavyBreathing;
    [SerializeField] private AudioClip panickedBreathing;
    [SerializeField] private AudioClip panickedBreathing2;
    [SerializeField] private AudioClip explosion;

    [SerializeField] private AudioSource audioSource;

    private Vignette _playerCameraPostProcessVignette;
    private Vignette _deathCamVignette;
    private CustomController _playerController;
    private RobotVoiceController _robotVoiceController;
    private static float _timeLeft;
    private static int _previousPlayerCount;
    private bool _shouldCallForOxygenConsumption = false;
    private bool _needToInvokeOxygenConsumption = true;

    private bool isTimerOver = false;

    private bool hasSecondPlayerEnteredRoom = false;

    // private TextMeshProUGUI timerTextBox;
    private bool _canConsumeOxygen = false;
    private bool _needToInitiateTimer = false;

    private PhotonView _photonView;

    void Start()
    {
        // Big problème : le timer se reset à chaque entrée d'un deuxième joueur
        _timeLeft = timeLimit + 1;
        _robotVoiceController = GameObject.FindWithTag("RobotVoice").GetComponent<RobotVoiceController>();
        blackScreen.SetAlphaToZero();
        _playerController = GetComponentInParent<CustomController>();
        playerCamera.GetComponent<PostProcessVolume>().profile.TryGetSettings(out _playerCameraPostProcessVignette);
        deathCam.GetComponent<PostProcessVolume>().profile.TryGetSettings(out _deathCamVignette);
        oxygenBar.SetActive(false);

        _photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > _previousPlayerCount) //from 1 to 2 player
            {
                //RPC
                _photonView.RPC(nameof(InitiateTimer), RpcTarget.All); //Both player with same TIMER
            }

            if (_canConsumeOxygen || _shouldCallForOxygenConsumption)
            {
                _timeLeft -= Time.deltaTime / 2;
            }
        }

        if (_needToInitiateTimer)
        {
            Invoke(nameof(InitiateTimer), timeBeforeOxygenStart);
            _needToInitiateTimer = false;
        }
        
        Debug.Log("Mr. CHrono is now at " + _timeLeft);
        CheckTimer();
        UpdateOxygenBar();
        _previousPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    private void CheckTimer()
    {
        if (_timeLeft < timeLimit && _shouldCallForOxygenConsumption) //time left was initiated at timeLimit + timeBeforeOxygenStart
        {
            Debug.Log("wi" + _timeLeft +"  "+timeLimit);
            _shouldCallForOxygenConsumption = false;
            StartConsumingOxygen();
        }
        if (_timeLeft < 0 && !isTimerOver)
        {
            TimerIsOut();
        }
    }
    
    private void StartConsumingOxygen()
    {
        oxygenBar.SetActive(true);
        _canConsumeOxygen = true;
        audioSource.PlayOneShot(explosion);
        _playerController.LockAllMovement();
        StartCoroutine(CameraShake(8, 2, 0.04f));
    }
    
    [PunRPC]
    private void InitiateTimer()
    {
        Debug.Log(_timeLeft);
        _shouldCallForOxygenConsumption = true;
        _timeLeft = timeLimit + timeBeforeOxygenStart;
    }
    


    IEnumerator CameraShake(int numberOfCycle, float shakeDuration, float movement)
    {
        float startTime = Time.time;
        float BParam = 2f * (float) Math.PI * numberOfCycle / shakeDuration;

        while (_playerController.IsInCriticalMotion())
        {
            yield return null;
        }

        float camPosY = playerCamera.transform.position.y;
        float camPosZ = playerCamera.transform.position.z;
        float camPosX = playerCamera.transform.position.x;
        while ((Time.time - startTime) < shakeDuration)
        {
            float sinValue = Mathf.Sin(Time.time * BParam); // -1 1
            float newPosX = camPosX + movement * sinValue;
            float newPosY = camPosY + movement * sinValue;
            playerCamera.transform.position = new Vector3(newPosX, newPosY, camPosZ);
            yield return null;
        }

        playerCamera.transform.position = new Vector3(camPosX, camPosY, camPosZ);
        _playerController.UnlockAllMovement();
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

        Invoke(nameof(FinalBlackout), 1f);
        Invoke(nameof(GameIsLost), delayBeforeGameEnd);
    }

    private void PlayerDeathInitialPhase()
    {
        _playerController.disableMovement();
        PanickedBreathing();

        ShortBlackout();

        Invoke(nameof(PlayDeathAnimation), shortBlackoutTime);
    }

    private void PlayDeathAnimation()
    {
        if (!_photonView.IsMine) return;
        _playerController.PlayDeathAnimation();

        playerCamera.SetActive(false);
        deathCam.SetActive(true);

        _deathCamVignette.intensity.value = 1f;
        StartCoroutine(FadeIntensityDownToValue(_deathCamVignette, 0f, shortBlackoutTime));

        blackScreen.FadeToZeroAlpha();
    }

    private void ShortBlackout()
    {
        blackScreen.SetFadingTime(shortBlackoutTime / 2f);
        blackScreen.FadeToFullAlpha();
        StartCoroutine(FadeIntensityUpToValue(_playerCameraPostProcessVignette, 1.0f, shortBlackoutTime));
    }

    private void PartialBlackout()
    {
        blackScreen.SetFadingTime(shortBlackoutTime / 2f);
        blackScreen.FadeToFullAlpha();
        StartCoroutine(FadeIntensityUpToValue(_playerCameraPostProcessVignette, 0.7f, shortBlackoutTime));
    }


    private void FinalBlackout()
    {
        StartCoroutine(FadeIntensityUpToValue(_deathCamVignette, 1f, longBlackoutTime));
        blackScreen.SetFadingTime(2f * delayBeforeGameEnd / 3f);
        blackScreen.FadeToFullAlpha();
        Invoke(nameof(HeavyBreathing), 2f);
        Invoke(nameof(PanickedBreathing2), 4f);
        Invoke(nameof(PanickedBreathing), 10f);
        Invoke(nameof(HeavyBreathing), 13f);
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

    IEnumerator FadeIntensityUpToValue(Vignette vignette, float intensityValue, float duration)
    {
        float startTime = Time.time;
        float initialIntensity = vignette.intensity.value;
        float intensityDifferential = intensityValue - initialIntensity;
        while (vignette.intensity < intensityValue)
        {
            vignette.intensity.value = initialIntensity + intensityDifferential * (Time.time - startTime) / duration;
            yield return null;
        }
    }

    IEnumerator FadeIntensityDownToValue(Vignette vignette, float intensityValue, float duration)
    {
        float startTime = Time.time;
        float initialIntensity = vignette.intensity.value;
        float intensityDifferential = intensityValue - initialIntensity;
        while (vignette.intensity > intensityValue)
        {
            vignette.intensity.value = initialIntensity + intensityDifferential * (Time.time - startTime) / duration;
            yield return null;
        }
    }

    public void OnMusicChange()
    {
        PlayOxygenRunningLowBlackout();
    }

    private void PlayOxygenRunningLowBlackout()
    {
        _playerController.disableMovement();
        PanickedBreathing();
        PartialBlackout();

        Invoke(nameof(ReturnFromBlackout), shortBlackoutTime);
        Invoke(nameof(ShortBlackout), shortBlackoutTime * 2f);
        Invoke(nameof(ReturnFromBlackout), shortBlackoutTime * 3f);
        Invoke(nameof(EndOxygenRunningLowBlackout), shortBlackoutTime * 4f);
    }

    private void ReturnFromBlackout()
    {
        StartCoroutine(FadeIntensityDownToValue(_playerCameraPostProcessVignette, 0f, shortBlackoutTime));
        blackScreen.SetFadingTime(shortBlackoutTime / 2f);
        blackScreen.FadeToZeroAlpha();
    }

    private void EndOxygenRunningLowBlackout()
    {
        _playerController.allowMovement();
        HeavyBreathing();
    }

    private void PanickedBreathing()
    {
        audioSource.PlayOneShot(panickedBreathing);
    }

    private void PanickedBreathing2()
    {
        audioSource.PlayOneShot(panickedBreathing2);
    }

    private void HeavyBreathing()
    {
        audioSource.PlayOneShot(heavyBreathing);
    }
}