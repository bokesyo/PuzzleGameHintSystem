using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HookInput : MonoBehaviour
{
    // 单例
    public static HookInput Single;

    /// 存储键盘按下时的响应事件
    /// 字典的键值为 响应事件对应的 按键
    /// 字典的元素为 响应事件
    /// 这里响应事件是 Action， 也可以通过 delegate 进行更丰富的定义。

    private Dictionary<KeyCode, Action> keyDownDic = new Dictionary<KeyCode, Action>();

    /// 存储按键抬起时的响应事件

    private Dictionary<KeyCode, Action> keyUpDic = new Dictionary<KeyCode, Action>();

    /// 存储鼠标移到时的回调
    /// 该回调需要一个 Vector3 参数， 该参数在 hook 调用时会传入 鼠标在一帧中的移动量

    private Action<Vector3> mouseMoveList = (movement) => { };

    /// 存储鼠标响应事件
    /// 这类事件是在鼠标 移动、点击左键、点击右键、点击中间等鼠标事件触发时都会被调用

    private Action mouseEventCall = () => { };

    /// 存储鼠标左键按下时的回调

    private Action mouseClickCall = () => { };

    /// 存储鼠标左键抬起时的回调

    private Action mouseReleaseCall = () => { };
    // 简单单例的构造
    private void Awake()
    {
        if (Single != null)
        {
            Destroy(Single.gameObject);
        }
        Single = this;
    }

    /// 注册鼠标移动时的回调

    /// <param name="callBack">回调</param>
    public void RegisterMouseMoveCallBack(Action<Vector3> callBack)
    {
        mouseMoveList += callBack;
    }

    /// 注册鼠标事件回调

    /// <param name="callBack">回调</param>
    public void RegisterMouseEventCallBack(Action callBack)
    {
        mouseEventCall += callBack;
    }

    /// 注册鼠标左键按下时的回调

    /// <param name="callBack">回调</param>
    public void RegisterMouseClickCallBack(Action callBack)
    {
        mouseClickCall += callBack;
    }

    /// 注册鼠标左键抬起时的回调

    /// <param name="callBack"></param>
    public void RegisterMouseRelaeaseCallBack(Action callBack)
    {
        mouseReleaseCall += callBack;
    }

    /// hook 用于调用鼠标移动回调的函数

    /// <param name="movement"></param>
    public void MouseMoveCallBack(Vector3 movement)
    {
        mouseMoveList.Invoke(movement);
    }

    /// hook 用于调用鼠标事件回调的函数

    public void MouseEventCallBack()
    {
        mouseEventCall.Invoke();
    }

    /// hook 用于调用鼠标左键按下时回调的函数
  
    public void MouseClickCallBack()
    {
        mouseClickCall.Invoke();
    }

    /// hook 用于调用鼠标左键抬起时回调的函数

    public void MouseReleaseCallBack()
    {
        mouseReleaseCall.Invoke();
    }

    /// 注册按键按下时的回调

    /// <param name="key">检测的按键</param>
    /// <param name="callBack">回调</param>
    public void RegisterKeyDownCallBack(KeyCode key, Action callBack)
    {
        if (!keyDownDic.ContainsKey(key)) 
            keyDownDic[key] = callBack;
        else keyDownDic[key] += callBack;
    }

    /// 注册按键抬起时的回调

    /// <param name="key">检测的按键</param>
    /// <param name="callBack">回调</param>
    public void RegisterKeyUpCallBack(KeyCode key, Action callBack)
    {
        if (!keyUpDic.ContainsKey(key)) keyUpDic[key] = callBack;
        else keyUpDic[key] += callBack;
    }

    /// hook 用于调用按键按下时回调的函数

    /// <param name="key"></param>
    public void KeyDownCallBack(KeyCode key)
    {
        if (keyDownDic.ContainsKey(key)) keyDownDic[key].Invoke();
    }

    /// hook 用于调用按键抬起时回调的函数

    /// <param name="key"></param>
    public void KeyUpCallBack(KeyCode key)
    {
        if (keyUpDic.ContainsKey(key)) keyUpDic[key].Invoke();
    }
}