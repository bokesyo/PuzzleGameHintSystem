using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCam;
    // Start is called before the first frame update
    public void MyStart()
    {
        canvas.SetActive(true);
        Debug.Log($"playerPrefab.name: {playerPrefab.name}");
        GameObject p =  PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(playerPrefab.transform.position.x, playerPrefab.transform.position.y), Quaternion.identity);
        sceneCam.SetActive(false);
        Debug.Log($"sceneCam: {sceneCam.name}");
        Debug.Log($"player: {p.name}");

    }

    // Update is called once per frame
    void Update()
    {
        
        //canvas.SetActive(false);
        
    }
}
