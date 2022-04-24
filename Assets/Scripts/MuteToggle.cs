using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MuteToggle : MonoBehaviour
{
    public Toggle m_Toggle;
    public GameObject AudioSource;
    public PhotonView photonView;

    void Start()
    {
        m_Toggle.onValueChanged.AddListener(ToggleOnValueChanged);
    }

    private void ToggleOnValueChanged(bool isOn)
    {
        if (isOn)
        {
            photonView.RPC("MuteVoice", RpcTarget.AllBuffered, true);
        }
        else
        {
            photonView.RPC("MuteVoice", RpcTarget.AllBuffered, false);
        }
    }

    [PunRPC]
    void MuteVoice(bool mute)
    {
        AudioSource.GetComponent<AudioSource>().mute = mute;
    }
}
