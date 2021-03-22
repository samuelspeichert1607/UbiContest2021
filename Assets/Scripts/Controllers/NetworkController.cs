using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public string SceneNameToLoadOnStart;
    public int MaxPlayers;
}

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text txtStatus = null;

    [SerializeField]
    private GameObject[] btnStarts = null;

    [SerializeField]
    private byte MaxPlayers = 4;

    [SerializeField]
    private List<DefaultRoom> defaultRooms;

    private string chosenRoomName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        ChangeStateOfButton(false);
        Status("Connecting to Server");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.AutomaticallySyncScene = true;
        ChangeStateOfButton(true);
        Status("Connecting to " + PhotonNetwork.ServerAddress);
    }

    public void btnStart_Click(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];

        chosenRoomName = defaultRooms[defaultRoomIndex].SceneNameToLoadOnStart;

        string roomName = roomSettings.Name;
        RoomOptions opts = new RoomOptions();
        opts.IsOpen = true;
        opts.IsVisible = true;
        opts.MaxPlayers = (byte)roomSettings.MaxPlayers;

        PhotonNetwork.JoinOrCreateRoom(roomName, opts, Photon.Realtime.TypedLobby.Default);
        ChangeStateOfButton(false);
        Status("Joining " + roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        SceneManager.LoadScene(chosenRoomName);
        // //TODO only until game includes all piece
        // SceneManager.LoadScene("synchroLevel_v01", LoadSceneMode.Additive);
    }

    private void Status(string msg)
    {
        Debug.Log(msg);
        txtStatus.text = msg;
    }


    private void ChangeStateOfButton(bool state)
    {
        foreach (GameObject btnStart in btnStarts)
        {
            btnStart.SetActive(state);
        }
    }
}
