using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public string SceneNameToLoadOnStart;
    public int NumberOfCurrentPlayers = 0;
    public int MaxPlayers = 2;
}

public class NetworkController : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI txtStatus = null;
    [SerializeField] private GameObject onOpenFirstSelected;
    [SerializeField] private AudioPlayerMenu audioPlayer;

    [SerializeField]
    private GameObject[] btnStarts = null;
    
    [SerializeField]
    private List<DefaultRoom> defaultRooms;

    private string chosenRoomName;
    private GameObject _currentlySelected;
    private bool isAtfirstOpeningFrame;

    private void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        
    }

    private void Start()
    {
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
            audioPlayer.PlayButtonNavigationSound();
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
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        ChangeStateOfButton(true);
        Status("Connecting to " + PhotonNetwork.ServerAddress);
    }

    public void btnStart_Click(int defaultRoomIndex)
    {
        if (defaultRooms[defaultRoomIndex].MaxPlayers != defaultRooms[defaultRoomIndex].NumberOfCurrentPlayers)
        {
            audioPlayer.PlayClickSound();
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
        else
        {
            Debug.Log("La salle est pleine. Veuillez en choisir une autre.");
        }
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

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        foreach (RoomInfo roomInfo in roomList)
        {
            int i = 0;
            foreach (DefaultRoom defaultRoom in defaultRooms)
            {
                if(roomInfo.Name == defaultRoom.Name)
                {
                    defaultRoom.NumberOfCurrentPlayers = roomInfo.PlayerCount;
                    defaultRoom.MaxPlayers = roomInfo.MaxPlayers;

                }



                i++;
            }
            Debug.Log(" RoomName : " + roomInfo.Name + " Nombres de joueurs présents : " + roomInfo.PlayerCount);
        }

        for(int i = 0; i < btnStarts.Length; i++)
        {
            btnStarts[i].GetComponentInChildren<TextMeshProUGUI>().text =
                defaultRooms[i].Name
                + " "
                + defaultRooms[i].NumberOfCurrentPlayers
                + "/"
                + defaultRooms[i].MaxPlayers;

            if(defaultRooms[i].NumberOfCurrentPlayers >= defaultRooms[i].MaxPlayers)
            {
                btnStarts[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                btnStarts[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
        }

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
    
    private void SelectObject(GameObject gameObjectToSelect)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
    }
    
}
