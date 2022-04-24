using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class UseButtonToggle : MonoBehaviour
{
    public GameObject Controller;
    public Toggle m_Toggle;

    public PhotonView photonView;
    public GameObject Buuttons1;
    public GameObject Buuttons2;
    public GameObject Buuttons3;

    void Start()
    {
        m_Toggle.onValueChanged.AddListener(ToggleOnValueChanged);
    }

    private void ToggleOnValueChanged(bool isOn)
    {
        if (isOn)
        {
            Controller.GetComponent<ChatManager>().UseHintButton = true;
        }
        else
        {
            Controller.GetComponent<ChatManager>().UseHintButton = false;
            photonView.RPC("DisableButton", RpcTarget.AllBuffered);
        }
    }




    [PunRPC]
    void DisableButton()
    {
        Buuttons1.SetActive(false);
        Buuttons2.SetActive(false);
        Buuttons3.SetActive(false);
    }
}
