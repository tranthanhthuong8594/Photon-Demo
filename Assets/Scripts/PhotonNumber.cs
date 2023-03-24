using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;

public class PhotonNumber : MonoBehaviourPun, IPunObservable
{
    public TextMeshPro textNumber;
    [SerializeField] public int number = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonSerializeView");
        if (stream.IsWriting) this.StreamWriting(stream);
        else this.StreamReading(stream, info);
    }

    private void StreamReading(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("StreamWriting");
        this.number = (int)stream.ReceiveNext();
        this.textNumber.text = this.number.ToString();
    }

    private void StreamWriting(PhotonStream stream)
    {
        Debug.Log("StreamWriting");
        stream.SendNext(this.number);
    }

    public virtual void Set(int number)
    {
        this.number = number;
        this.textNumber.text = number.ToString();
    }

    public virtual void OnClaim()
    {
        Debug.Log(transform.name + " OnClaim: " + this.number);

        if (!PhotonNumberLimit.instance.CanClaim(this.number)) return;

        object[] datas = new object[] { this.number };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

        PhotonPlayer.me.numberCount++;
        PhotonPlayer.me.SetNameText();
        PhotonNetwork.RaiseEvent(
            ((byte)EventCode.onNumberClaimed),
            datas,
            raiseEventOptions,
            SendOptions.SendUnreliable
            );
    }

    internal void Claimed()
    {
        Debug.Log("Claimed: " + this.number);
        gameObject.SetActive(false);
    }
}
