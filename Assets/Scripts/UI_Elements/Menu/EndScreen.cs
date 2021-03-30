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
    
    void Start()
    {
        textToAppear.StartCoroutine("Start");
        textToAppear.SetAlphaToZero();
        textToAppear.FadeToFullAlpha();
        pulsateText.enabled = false;
    }

    void Update()
    {
        if (textToAppear.IsTransitionDone())
        {
            pulsateText.enabled = true;
            if (Input.anyKeyDown)
            {
                Invoke(nameof(LoadMenu), loadDelay);
            }
        }
    }

    private void LoadMenu()
    {
        //TODO probably should adapt this to a better way
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("principalMENU");
    }
}
