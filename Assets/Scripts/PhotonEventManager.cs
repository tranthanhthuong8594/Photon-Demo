using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

public class PhotonEventManager : MonoBehaviour
{
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += this.EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= this.EventReceived;
    }

    private void EventReceived(EventData obj)
    {
        Debug.Log("EventReceived: " + obj.Code.ToString());
        if (obj.Code == (byte)EventCode.onNumberClaimed) this.OnEventNumberClaimed(obj);
    }

    private void OnEventNumberClaimed(EventData obj)
    {
        object[] datas = (object[])obj.CustomData;
        int number = (int) datas[0];
        Debug.Log("OnNumberClaimed: " + number);
        GameManager.instance.NumberOnClaimed(number);
    }
}
