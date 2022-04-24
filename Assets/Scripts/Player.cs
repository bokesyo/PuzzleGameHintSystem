using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    public GameObject playerCam;

    void Awake()
    {
        if (photonView.IsMine)
        {
            playerCam.SetActive(true);
        }
    }

}
