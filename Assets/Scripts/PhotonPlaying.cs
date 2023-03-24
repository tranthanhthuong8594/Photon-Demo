using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class PhotonPlaying : MonoBehaviourPunCallbacks
{
    public static PhotonPlaying Instance;

    public string photonPlayerName = "PhotonPlayer";
    public List<PlayerProfile> players = new List<PlayerProfile>();

    private void Awake()
    {
        Instance = this;

        this.LoadRoomPlayers();
        this.SpawnPlayer();
    }

    protected virtual void SpawnPlayer()
    {
        if(PhotonNetwork.NetworkClientState != ClientState.Joined)
        {
            Invoke("SpawnPlayer", 1f);
            return;
        }

        GameObject playerObj = PhotonNetwork.Instantiate(this.photonPlayerName, Vector3.zero, Quaternion.identity);
        PhotonView photonView = playerObj.GetComponent<PhotonView>();
        if(photonView.IsMine)
        {
            PhotonPlayer photonPlayer = playerObj.GetComponent<PhotonPlayer>();
            PhotonPlayer.me = photonPlayer;
        }
    }

    protected virtual void LoadRoomPlayers()
    {
        if (PhotonNetwork.NetworkClientState != ClientState.Joined)
        {
            Invoke("LoadRoomPlayers", 1f);
            return;
        }

        PlayerProfile playerProfile;
        foreach(KeyValuePair<int, Player> playerData in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(playerData.Value.NickName);
            playerProfile = new PlayerProfile
            {
                nickName = playerData.Value.NickName
            };
            this.players.Add(playerProfile);
        }
    }

    public virtual void Leave()
    {
        if (!PhotonNetwork.InRoom) return;

        Debug.Log(transform.name + " : Leave Room " + name);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
        PhotonNetwork.LoadLevel("Home");
    }
}
