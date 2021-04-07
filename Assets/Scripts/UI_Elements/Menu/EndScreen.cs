using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private float loadDelay;
    [SerializeField] private FadableText textToAppear;
    [SerializeField] private PulsateText pulsateText;
    private bool hasStartedPulsating = false;

    void Start()
    {
        textToAppear.StartCoroutine("Start");
        textToAppear.SetAlphaToZero();
        textToAppear.FadeToFullAlpha();
        pulsateText.StartCoroutine("Start");
        pulsateText.SetAlphaToZero();
    }

    void Update()
    {
        if (textToAppear.IsTransitionDone())
        {
            if (!hasStartedPulsating)
            {
                pulsateText.StartPulsating();
                hasStartedPulsating = true;
            }
            if (Input.anyKeyDown)
            {
                Invoke(nameof(LoadMenu), loadDelay);
            }
        }
    }

    private void LoadMenu()
    {
        //TODO probably should adapt this to a better way -> should be in StatusHud
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("principalMENU");
    }
}
