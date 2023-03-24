using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickEvent : MonoBehaviour
{
    private void OnMouseDown()
    {
        PhotonNumber photonNumber = transform.parent.GetComponent<PhotonNumber>();
        photonNumber.OnClaim();
    }
}
