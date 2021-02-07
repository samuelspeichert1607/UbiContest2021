using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text txtStatus = null;

    [SerializeField]
    private GameObject btnStart = null;

    [SerializeField]
    private byte MaxPlayers = 4;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        btnStart.SetActive(false);
        Status("Connecting to Server");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.AutomaticallySyncScene = true;
        btnStart.SetActive(true);
        Status("Connecting to " + PhotonNetwork.ServerAddress);
    }

    public void btnStart_Click()
    {
        string roomName = "Room1";
        Photon.Realtime.RoomOptions opts = new Photon.Realtime.RoomOptions();
        opts.IsOpen = true;
        opts.IsVisible = true;
        opts.MaxPlayers = MaxPlayers;

        PhotonNetwork.JoinOrCreateRoom(roomName, opts, Photon.Realtime.TypedLobby.Default);
        btnStart.SetActive(false);
        Status("Joining " + roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        SceneManager.LoadScene("TestConnectivity");
    }

    private void Status(string msg)
    {
        Debug.Log(msg);
        txtStatus.text = msg;
    }
}
