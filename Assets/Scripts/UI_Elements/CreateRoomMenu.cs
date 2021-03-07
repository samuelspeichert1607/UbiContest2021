using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to Server");
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) { return; }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        //MasterManager.DebugConsole.AddText("Created room successfully.", this);
        Debug.Log("Room Created successfully.");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //MasterManager.DebugConsole.AddText("Created room failed : " + message, this);
        Debug.Log("Room Created failed : " + message);
    }
}
