using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;
    
#region Unity methods

private void Awake()
{
    PhotonNetwork.AutomaticallySyncScene = true;
}

// Start is called before the first frame update
    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
#endregion

#region Public methods
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            //check if connect to server or not
            //if not connect oserver
            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
#endregion

#region PUN callbacks
    //called when connect to a server 
    public override void OnConnectedToMaster()
    {
        Debug.Log($"{PhotonNetwork.NickName}==Connect to photon server");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }
    
    
    //called when has a connection
    public override void OnConnected()
    {
        Debug.Log($"Connected to the internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"{PhotonNetwork.NickName} joined to {PhotonNetwork.CurrentRoom.Name}");
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(
            $"{newPlayer.NickName} joined to {PhotonNetwork.CurrentRoom.Name} {PhotonNetwork.CurrentRoom.Players}");
    }

    #endregion    
    
#region Private methods

void CreateAndJoinRoom()
{
    string randomRoomName = "Room " + Random.Range(0, 10000);

    RoomOptions roomOptions = new RoomOptions();
    roomOptions.IsOpen = true;
    roomOptions.IsVisible = true;
    roomOptions.MaxPlayers = 5;

    PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
}
#endregion
}
