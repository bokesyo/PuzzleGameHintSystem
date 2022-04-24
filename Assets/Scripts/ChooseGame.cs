using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;


public class ChooseGame : MonoBehaviour
{
    private Dropdown dropdown;
    public GameObject Baba;
    public GameObject Gorogoa;
    public GameObject TheRoom2;
    public PhotonView photonView;

    private void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        DropdownItemSelected(dropdown);
    }


    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        photonView.RPC("ShowBubbleCanvas", RpcTarget.AllBuffered, index);
    }

    [PunRPC]
    void ShowBubbleCanvas(int index)
    {
        Baba.SetActive(false);
        Gorogoa.SetActive(false);
        TheRoom2.SetActive(false);

        switch (index)
        {
            case 0:
                print("ตฺ1าณ");
                Baba.SetActive(true);

                break;
            case 1:
                print("ตฺ2าณ");
                Gorogoa.SetActive(true);
                break;
            case 2:
                TheRoom2.SetActive(true);
                break;
        }
    }

}
