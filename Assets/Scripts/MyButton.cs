using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;


public class MyButton : MonoBehaviour
{
    private bool focus = false;
    public PhotonView photonView;
    public GameObject BubbleSpeech;
    private GameObject ClickTimelog;

    void Start()
    {
        HookInput.Single.RegisterMouseMoveCallBack((mouse) => IsFocus(mouse));
        HookInput.Single.RegisterMouseClickCallBack(OnClick);
        ClickTimelog = GameObject.Find("CurrentTime");
        Debug.Log(ClickTimelog.name);
    }

    private void IsFocus(Vector3 mousePosition)
    {
        Vector2 pos;
        pos.x = (-mousePosition.x - (float)960)/108; // / (960 / 5) * 16/9
        pos.y = (-mousePosition.y + (float)540)/108;  // 108 = 540 / 5

        //Debug.LogError($"mouse actual Position£º{mousePosition}, mousePosition£º{pos}, transform.position: {transform.position}");
        if (new Vector2(pos.x - transform.position.x, pos.y - transform.position.y).magnitude < 0.35f)
        {
            //Debug.LogError($"Focusing!!!");
            focus = true;
        }
        else
        {
            focus = false;
        }
    }
                                                                                                                                  

    private void OnClick()
    {
        Debug.Log("Click mouse");
        
        Debug.Log(DateTime.Now.ToString());
        //ClickTimelog.GetComponent<TimeLog>().StruggleTimeLog("lala");

        //photonView.RPC("ShowBubble", RpcTarget.AllBuffered, DateTime.Now.ToString());

        if (focus)
        {
            photonView.RPC("ShowBubble", RpcTarget.AllBuffered, DateTime.Now.ToString());

            //BubbleSpeech.SetActive(true);
            //Debug.Log("Click button");
        }
    }

    [PunRPC]
    void ShowBubble(string clicktime)
    {
        BubbleSpeech.SetActive(true);
        ClickTimelog.GetComponent<TimeLog>().StruggleTimeLog(clicktime);
        //MakeFile(header, clicklog, "ClickLog");
        //SaveCSV(clicktime, "ClickLog");
    }

}
