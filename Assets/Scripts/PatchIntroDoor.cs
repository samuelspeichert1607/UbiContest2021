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


    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (_doorsAreNotSetToOpen)
            {
                _doorsAreNotSetToOpen = false;
                Invoke(nameof(OpenIntroDoors), delayBeforeOpening);
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
}
