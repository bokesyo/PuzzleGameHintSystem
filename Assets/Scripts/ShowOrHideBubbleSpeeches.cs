using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ShowOrHideBubbleSpeeches : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject BubbleSpeech1;
    public GameObject BubbleSpeech2;
    public GameObject BubbleSpeech3;
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
            photonView.RPC("ActiveBubbleSpeech", RpcTarget.AllBuffered);
        }
        else
        {
            photonView.RPC("DisableBubbleSpeech", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void ActiveBubbleSpeech()
    {
        BubbleSpeech1.SetActive(true);
        BubbleSpeech2.SetActive(true);
        BubbleSpeech3.SetActive(true);
        Audio.SetActive(true);
    }

    [PunRPC]
    void DisableBubbleSpeech()
    {
        BubbleSpeech1.SetActive(false);
        BubbleSpeech2.SetActive(false);
        BubbleSpeech3.SetActive(false);
        Audio.SetActive(false);
    }
}
