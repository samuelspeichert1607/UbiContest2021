using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//https://www.youtube.com/watch?v=4yZNAuqkOB0&t=1114s&ab_channel=BarretGaylor
public class LobbyManager : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            //PhotonNetwork.j = false;

            PhotonNetwork.AutomaticallySyncScene = true;

            //PhotonNetwork.ConnectUsingSettings();

        }
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        
    }

    public override void OnJoinedLobby()
    {
        print("Connected To Lobby");
    }

    public void CreateRoom()
    {

    }
}
