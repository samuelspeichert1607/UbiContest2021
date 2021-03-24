using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public string SceneNameToLoadOnStart;
    public int MaxPlayers;
}

public class NetworkController : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI txtStatus = null;
    [SerializeField] private GameObject onOpenFirstSelected;
    [SerializeField] private AudioManagerMenu _audioManager;

    [SerializeField]
    private GameObject[] btnStarts = null;


    [SerializeField]
    private List<DefaultRoom> defaultRooms;

    private string chosenRoomName;
    private GameObject _currentlySelected;
    private bool isAtfirstOpeningFrame;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            Disconnect();
        }
        PhotonNetwork.ConnectUsingSettings();
        ChangeStateOfButton(false);
        Status("Connecting to Server");
        SelectObject(onOpenFirstSelected);
        isAtfirstOpeningFrame = true;
    }
    
    public void Update()
    {
        if (HasNavigatedInMenu())
        {
            _audioManager.PlayButtonNavigationSound();
        }
        _currentlySelected = EventSystem.current.currentSelectedGameObject;
        if (isAtfirstOpeningFrame) isAtfirstOpeningFrame = false;
    }

    private bool HasNavigatedInMenu()
    {
        return _currentlySelected != EventSystem.current.currentSelectedGameObject && !isAtfirstOpeningFrame;
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
        _audioManager.PlayClickSound();
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];

        chosenRoomName = defaultRooms[defaultRoomIndex].SceneNameToLoadOnStart;

        string roomName = roomSettings.Name;
        RoomOptions opts = new RoomOptions();
        opts.IsOpen = true;
        opts.IsVisible = true;
        opts.MaxPlayers = (byte)roomSettings.MaxPlayers;

        PhotonNetwork.JoinOrCreateRoom(roomName, opts, TypedLobby.Default);
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

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("returnCode : " + returnCode);
        Debug.Log("message : " + returnCode);
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

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    private void SelectObject(GameObject gameObjectToSelect)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
    }
    
}
