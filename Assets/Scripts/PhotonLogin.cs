using Photon.Pun;
using TMPro;
using UnityEngine;

public class PhotonLogin : MonoBehaviourPunCallbacks
{
    public TMP_InputField userName;

    void Start()
    {
        this.userName.text = "Ni";
    }

    public virtual void Login()
    {
        if (PhotonNetwork.IsConnected) return;

        string name = this.userName.text;
        Debug.Log(transform.name + " : Login " + name);

        PhotonNetwork.AutomaticallySyncScene = true;    
        PhotonNetwork.LocalPlayer.NickName = name;
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
    }
}
