using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using System.Collections.Generic;
using System;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public static PhotonRoom Instance;
    public TMP_InputField roomName;
    public Transform roomContent;
    public UIRoomProfile roomPrefab;
    public List<RoomInfo> updatedRooms;
    public List<RoomProfile> rooms = new List<RoomProfile>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.roomName.text = "Room 1";
    }

    public virtual void Create()
    {
        string name = this.roomName.text;
        Debug.Log(transform.name + " : Create Room " + name);
        PhotonNetwork.CreateRoom(name);
    }
    
    public virtual void Join()
    {
        if (PhotonNetwork.InRoom) return;

        string name = this.roomName.text;
        Debug.Log(transform.name + " : Join Room " + name);
        PhotonNetwork.JoinRoom(name);
        this.ClearRoomUI();
    }

    private void ClearRoomUI()
    {
        foreach (Transform child in this.roomContent)
        {
            Destroy(child.gameObject);
        }
    }

    public virtual void Leave()
    {
        if (!PhotonNetwork.InRoom) return;

        Debug.Log(transform.name + " : Leave Room " + name);
        PhotonNetwork.LeaveRoom();
    }

    public virtual void StartGame()
    {
        Debug.Log(transform.name + " : Start Game " + name);

        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("GamePlay");
        else Debug.LogWarning("You are not Master Client!");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        this.updatedRooms = roomList;

        foreach(RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList) this.RoomRemove(roomInfo);
            else this.RoomAdd(roomInfo);
        }

        this.UpdateRoomProfileUI();
    }

    public virtual void UpdateRoomProfileUI()
    {
        this.ClearRoomUI();

        foreach(RoomProfile roomProfile in this.rooms)
        {
            UIRoomProfile uiRoomProfile = Instantiate(this.roomPrefab);
            uiRoomProfile.SetRoomProfile(roomProfile);
            uiRoomProfile.transform.SetParent(this.roomContent);
            uiRoomProfile.transform.localScale = Vector3.one;
        }

        Debug.Log(this.rooms.Count);
    }

    protected virtual void RoomAdd(RoomInfo roomInfo)
    {
        RoomProfile roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile != null) return;
        roomProfile = new RoomProfile
        {
            name = roomInfo.Name
        };
        this.rooms.Add(roomProfile);
    }

    protected virtual void RoomRemove(RoomInfo roomInfo)
    {
        RoomProfile roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile == null) return;
        this.rooms.Remove(roomProfile);
    }

    protected virtual RoomProfile RoomByName(string name)
    {
        if (this.rooms == null) return null;
        foreach(RoomProfile roomProfile in this.rooms)
        {
            if (roomProfile.name == name) return roomProfile;
        }
        return null;
    }
}
