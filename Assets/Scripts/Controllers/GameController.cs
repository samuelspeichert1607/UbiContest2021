using Photon.Pun;
using UnityEngine;

public class GameController : MonoBehaviourPun
{
    [SerializeField]
    private Transform[] spawnPoints = null;

    private void Awake()
    {
        int i = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        PhotonNetwork.Instantiate("Player", spawnPoints[i].position, spawnPoints[i].rotation);
    }
}
