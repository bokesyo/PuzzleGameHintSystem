using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    public int hideAfterSeconds;
    public GameObject gameObject;

    void OnEnable()
    {
        hideAfterSeconds = 20;
        if (hideAfterSeconds > 0)
        {
            Debug.Log("hide");
            StartCoroutine(hideSelf());
        }
    }


    IEnumerator hideSelf()
    {
        yield return new WaitForSeconds(hideAfterSeconds);
        gameObject.SetActive(false);
    }
}
