using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviourPun//, IPunObservable
{
    public PhotonView photonView;
    public GameObject BubbleSpeech;

    public GameObject HintTextMeshPro1;
    public GameObject HintTextMeshPro2;
    public GameObject HintTextMeshPro3;
    public GameObject HintTextMeshPro4;

    InputField HintInput;
    public bool UseHintButton = true;
    private bool DisableSend;

    private void Awake()
    {
        HintInput = GameObject.Find("HintInputField").GetComponent<InputField>();
        HintInput.interactable = true;
    }

    private void FixedUpdate()
    {
        //发送提示
        if (Input.GetKey(KeyCode.Return) && photonView.IsMine && HintInput.isFocused)
        {
            HintInput.ActivateInputField();
            if (HintInput.text != "")
            {
                 photonView.RPC("SendMsg", RpcTarget.AllBuffered, HintInput.text);
                 HintInput.text = "";
                 HintInput.ActivateInputField();
            }
        }
    }


    [PunRPC]
    void SendMsg(string msg)
    {
        HintTextMeshPro1.GetComponent<TMP_Text>().text = msg;
        HintTextMeshPro2.GetComponent<TMP_Text>().text = msg;
        HintTextMeshPro3.GetComponent<TMP_Text>().text = msg;
        HintTextMeshPro4.GetComponent<TMP_Text>().text = msg;
        //if (!UseHintButton)
        //{
        //    StartCoroutine(hideBubbleSpeech());
        //}
    }
    IEnumerator hideBubbleSpeech()
    {
        yield return new WaitForSeconds(30);
        BubbleSpeech.SetActive(false);
        //DisableSend = false;
    }

}


//Todo: 三个游戏提示文案不同颜色和位置适配