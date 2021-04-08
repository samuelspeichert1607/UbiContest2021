using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PatchIntroDoor : MonoBehaviour
{

    [SerializeField] private Actionable[] introDoors;

    [SerializeField] private float delayBeforeOpening = 57f;

    private bool _doorsAreNotSetToOpen = true;
    private int previousPlayerCount;

    private float timer =1;
    private PhotonView _photonView;
    private bool HasStarted = false;

    private bool mustStartTimer = true;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom == null) return;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2&& mustStartTimer)
        {
            mustStartTimer = false;
            _photonView.RPC(nameof(InitiateTimer), RpcTarget.All);
        }



        if (HasStarted)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            HasStarted = false;
            timer = 1;
            if (_doorsAreNotSetToOpen)
            {
                OpenIntroDoors();
                _doorsAreNotSetToOpen = false;

            }
        }
    }
    
    private void OpenIntroDoors()
    {
        foreach (Actionable door in introDoors)
        {
            door.OnAction();
        }
    }
    [PunRPC]
    private void InitiateTimer()
    {
        timer = delayBeforeOpening;
        HasStarted = true;
    }
}
