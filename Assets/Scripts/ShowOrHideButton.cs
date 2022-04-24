using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ShowOrHideButton : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject Buuttons1;
    public GameObject Buuttons2;
    public GameObject Buuttons3;
    public GameObject Audio;
    public bool show;


    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        if (show)
        {
            photonView.RPC("ActiveButton", RpcTarget.AllBuffered);
        }
        else
        {
            photonView.RPC("DisableButton", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void ActiveButton()
    {
        Buuttons1.SetActive(true);
        Buuttons2.SetActive(true);
        Buuttons3.SetActive(true);
        Audio.SetActive(true);
    }

    [PunRPC]
    void DisableButton()
    {
        Buuttons1.SetActive(false);
        Buuttons2.SetActive(false);
        Buuttons3.SetActive(false);
        Audio.SetActive(false);
    }
}
