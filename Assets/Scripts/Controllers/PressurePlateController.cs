using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{

    [SerializeField] public Actionable[] actionableObject;
    [SerializeField] private float penalityTimer;
    public bool unlockedPlates = true;

    private Color initialColor;
    private PhotonView _photonView;

    void Start()
    {
        initialColor = GetComponentInChildren<Renderer>().material.color;
        _photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    private void wonRPC()
    {
        ChangeColor(Color.green);
        unlockedPlates = false;
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }
    }

    public void won()
    {
        _photonView.RPC(nameof(wonRPC), RpcTarget.All);
    }
    [PunRPC]
    private void penalityRPC()
    {
        ChangeColor(Color.red);
        unlockedPlates = false;
        Invoke(nameof(resetPlates), penalityTimer);

    }
    public void penality()
    {
        _photonView.RPC(nameof(penalityRPC), RpcTarget.All);
    }
    private void resetPlates()
    {
        _photonView.RPC(nameof(resetPlatesRPC), RpcTarget.All);
    }
    [PunRPC]
    private void resetPlatesRPC()
    {
        ChangeColor(initialColor);
        unlockedPlates = true;
    }

    private void ChangeColor(Color color)
    {
        foreach (Transform child in transform)
        {
            child.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
        }
    }
}
