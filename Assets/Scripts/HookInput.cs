using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HookInput : MonoBehaviour
{
    // ����
    public static HookInput Single;

    /// �洢���̰���ʱ����Ӧ�¼�
    /// �ֵ�ļ�ֵΪ ��Ӧ�¼���Ӧ�� ����
    /// �ֵ��Ԫ��Ϊ ��Ӧ�¼�
    /// ������Ӧ�¼��� Action�� Ҳ����ͨ�� delegate ���и��ḻ�Ķ��塣

    private Dictionary<KeyCode, Action> keyDownDic = new Dictionary<KeyCode, Action>();

    /// �洢����̧��ʱ����Ӧ�¼�

    private Dictionary<KeyCode, Action> keyUpDic = new Dictionary<KeyCode, Action>();

    /// �洢����Ƶ�ʱ�Ļص�
    /// �ûص���Ҫһ�� Vector3 ������ �ò����� hook ����ʱ�ᴫ�� �����һ֡�е��ƶ���

    private Action<Vector3> mouseMoveList = (movement) => { };

    /// �洢�����Ӧ�¼�
    /// �����¼�������� �ƶ���������������Ҽ�������м������¼�����ʱ���ᱻ����

    private Action mouseEventCall = () => { };

    /// �洢����������ʱ�Ļص�

    private Action mouseClickCall = () => { };

    /// �洢������̧��ʱ�Ļص�

    private Action mouseReleaseCall = () => { };
    // �򵥵����Ĺ���
    private void Awake()
    {
        if (Single != null)
        {
            Destroy(Single.gameObject);
        }
        Single = this;
    }

    /// ע������ƶ�ʱ�Ļص�

    /// <param name="callBack">�ص�</param>
    public void RegisterMouseMoveCallBack(Action<Vector3> callBack)
    {
        mouseMoveList += callBack;
    }

    /// ע������¼��ص�

    /// <param name="callBack">�ص�</param>
    public void RegisterMouseEventCallBack(Action callBack)
    {
        mouseEventCall += callBack;
    }

    /// ע������������ʱ�Ļص�

    /// <param name="callBack">�ص�</param>
    public void RegisterMouseClickCallBack(Action callBack)
    {
        mouseClickCall += callBack;
    }

    /// ע��������̧��ʱ�Ļص�

    /// <param name="callBack"></param>
    public void RegisterMouseRelaeaseCallBack(Action callBack)
    {
        mouseReleaseCall += callBack;
    }

    /// hook ���ڵ�������ƶ��ص��ĺ���

    /// <param name="movement"></param>
    public void MouseMoveCallBack(Vector3 movement)
    {
        mouseMoveList.Invoke(movement);
    }

    /// hook ���ڵ�������¼��ص��ĺ���

    public void MouseEventCallBack()
    {
        mouseEventCall.Invoke();
    }

    /// hook ���ڵ�������������ʱ�ص��ĺ���
  
    public void MouseClickCallBack()
    {
        mouseClickCall.Invoke();
    }

    /// hook ���ڵ���������̧��ʱ�ص��ĺ���

    public void MouseReleaseCallBack()
    {
        mouseReleaseCall.Invoke();
    }

    /// ע�ᰴ������ʱ�Ļص�

    /// <param name="key">���İ���</param>
    /// <param name="callBack">�ص�</param>
    public void RegisterKeyDownCallBack(KeyCode key, Action callBack)
    {
        if (!keyDownDic.ContainsKey(key)) 
            keyDownDic[key] = callBack;
        else keyDownDic[key] += callBack;
    }

    /// ע�ᰴ��̧��ʱ�Ļص�

    /// <param name="key">���İ���</param>
    /// <param name="callBack">�ص�</param>
    public void RegisterKeyUpCallBack(KeyCode key, Action callBack)
    {
        if (!keyUpDic.ContainsKey(key)) keyUpDic[key] = callBack;
        else keyUpDic[key] += callBack;
    }

    /// hook ���ڵ��ð�������ʱ�ص��ĺ���

    /// <param name="key"></param>
    public void KeyDownCallBack(KeyCode key)
    {
        if (keyDownDic.ContainsKey(key)) keyDownDic[key].Invoke();
    }

    /// hook ���ڵ��ð���̧��ʱ�ص��ĺ���

    /// <param name="key"></param>
    public void KeyUpCallBack(KeyCode key)
    {
        if (keyUpDic.ContainsKey(key)) keyUpDic[key].Invoke();
    }
}