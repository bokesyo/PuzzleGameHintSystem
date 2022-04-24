using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool drag = false;
    private Vector3 lastMousePos;
    GameObject gameObj;

    void Start()
    {
        Application.targetFrameRate = 60;

        //HookInput.Single.RegisterMouseClickCallBack(() => Move(wPoint));
    }

    private void OnMouseDrag()
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Debug.LogError($"Pos{pos.x}{pos.y}");
        Vector3 delta = pos - lastMousePos;
        transform.position += delta;

        lastMousePos = pos;
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Debug.LogError($"Pos{pos.x}{pos.y}");
    //        RaycastHit hitInfo;
    //        if (Physics.Raycast(ray, out hitInfo))
    //        {
    //            ////划出射线，只有在scene视图中才能看到
    //            Debug.DrawLine(ray.origin, hitInfo.point);


    //            gameObj = hitInfo.collider.gameObject;
    //            Debug.LogError("click object name is " + gameObj.name);

    //            if (gameObj.tag == "Drag")
    //            {
    //                Debug.LogError("pickup!");
    //                drag = true;
    //                lastMousePos = pos;
    //            }
    //        }


    //        //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        //Debug.LogError($"mouse{pos.x} { pos.y}");
    //        //pos.z = 0;

    //        //float distance = (pos - transform.position).magnitude;
    //        //if(distance < 1.0f)
    //        //{
    //        //    drag = true;
    //        //    lastMousePos = pos;
    //        //}
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        gameObj = null;
    //        drag = false;
    //    }

    //    if (drag)
    //    {
    //        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        pos.z = 0;

    //        Vector3 delta = pos - lastMousePos;
    //        transform.position += delta;

    //        lastMousePos = pos;
    //    }
        
    //}

}
