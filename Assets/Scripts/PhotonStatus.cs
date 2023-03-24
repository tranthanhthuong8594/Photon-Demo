using Photon.Pun;
using TMPro;
using UnityEngine;

public class PhotonStatus : MonoBehaviour
{
    public string photonStatus;
    public TextMeshProUGUI textStatus;

    void Update()
    {
        this.photonStatus = PhotonNetwork.NetworkClientState.ToString();
        this.textStatus.text = this.photonStatus;
    }
}
