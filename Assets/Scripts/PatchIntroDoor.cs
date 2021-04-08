using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PatchIntroDoor : MonoBehaviour
{

    [SerializeField] private Actionable[] introDoors;

    [SerializeField] private float delayBeforeOpening = 60f;
    // Start is called before the first frame update


    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Invoke(nameof(OpenIntroDoors), delayBeforeOpening);            
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
