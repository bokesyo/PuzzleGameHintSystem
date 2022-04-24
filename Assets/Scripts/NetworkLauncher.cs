using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    //public GameObject playerPrefab;
    //public GameObject sceneCam;
    public GameObject roomListUI;
    public InputField roomName;
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        //roomListUI.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        
        PhotonNetwork.JoinLobby();

        if (PhotonNetwork.InLobby)
        {
            Debug.Log("Welcome");
            roomListUI.SetActive(true);
        }
    }

    public void JoinOrCreateButton()
    {
        if (roomName.text.Length < 1)
            return;

        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(1);
    }
}
