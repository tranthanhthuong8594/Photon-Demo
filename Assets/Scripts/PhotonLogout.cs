using Photon.Pun;
using UnityEngine;

public class PhotonLogout : MonoBehaviour
{
    public virtual void Logout()
    {
        if (!PhotonNetwork.IsConnected) return;

        PhotonNetwork.Disconnect();
        Debug.Log(transform.name + " : Logout");
    }
}
