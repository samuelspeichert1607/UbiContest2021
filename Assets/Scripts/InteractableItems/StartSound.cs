using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class StartSound : MonoBehaviour
{
    private bool canPlaySound=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (canPlaySound && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            canPlaySound = false;
            Invoke("PlaySound",2);
        }
    }
    // Update is called once per frame

    //
    private void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
