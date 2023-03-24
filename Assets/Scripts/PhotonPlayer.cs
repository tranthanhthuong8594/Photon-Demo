using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ExitGames.Client.Photon;


public class PhotonPlayer : MonoBehaviourPun, IPunObservable
{
    public static PhotonPlayer me;

    public PhotonView photonView;
    public TextMeshPro nickNameLable;
    public string photonNickName = "Offline";

    public Vector3 mouseInput;
    public Vector3 mousePos;
    public Vector2 xPosLimit = new Vector2(-7, 7);
    public Vector2 yPosLimit = new Vector2(-4, 3);

    public int numberCount = 0;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        this.OwnerController();
        this.LoadOwnerNickName();
    }

    private void OwnerController()
    {
        if (this.photonView.ViewID != 0 && !this.photonView.IsMine) return;

        this.LoadMousePos();
        this.FollowMousePos();
    }

    private void LoadOwnerNickName()
    {
        SetNameText();
        if (this.photonView.ViewID == 0) return;
        this.photonNickName = this.photonView.Owner.NickName;
    }

    private void LoadMousePos()
    {
        this.mouseInput = Input.mousePosition;
        this.mouseInput.z = Camera.main.nearClipPlane;
        this.mousePos = Camera.main.ScreenToWorldPoint(this.mouseInput);
    }

    private void FollowMousePos()
    {
        Vector3 newPos = this.mousePos;
        newPos.z = 0;

        if (newPos.x > this.xPosLimit.y) newPos.x = this.xPosLimit.y;
        if (newPos.x < this.xPosLimit.x) newPos.x = this.xPosLimit.x;

        if (newPos.y > this.yPosLimit.y) newPos.y = this.yPosLimit.y;
        if (newPos.y < this.yPosLimit.x) newPos.y = this.yPosLimit.x;

        transform.position = newPos;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonSerializeView");
        if (stream.IsWriting) this.StreamWriting(stream);
        else this.StreamReading(stream);
    }

    private void StreamReading(PhotonStream stream)
    {
        this.numberCount = (int)stream.ReceiveNext();
    }

    private void StreamWriting(PhotonStream stream)
    {
        stream.SendNext(this.numberCount);
    }

    public void SetNameText()
    {
        this.nickNameLable.text = this.photonNickName + ": " + numberCount.ToString();
    }
}
