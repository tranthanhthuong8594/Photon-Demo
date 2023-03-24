using System;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIRoomProfile : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI roomName;
    [SerializeField] protected RoomProfile roomProfile;

    public virtual void SetRoomProfile(RoomProfile roomProfile)
    {
        this.roomProfile = roomProfile;
        this.roomName.text = this.roomProfile.name;
    }

    public virtual void OnClick()
    {
        PhotonRoom.Instance.roomName.text = this.roomProfile.name;
    }
}