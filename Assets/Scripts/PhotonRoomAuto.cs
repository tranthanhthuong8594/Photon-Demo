using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class PhotonRoomAuto : MonoBehaviourPunCallbacks
{
    public string nickName = "AutoName";
    public string roomName = "AutoRoom";

    void Awake()
    {
        this.AutoLogin();
    }

    private void AutoLogin()
    {
        if (PhotonNetwork.NetworkClientState == ClientState.Joined) return;

        PhotonNetwork.LocalPlayer.NickName = this.nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby!");
        PhotonNetwork.CreateRoom(this.roomName);
    }
}
