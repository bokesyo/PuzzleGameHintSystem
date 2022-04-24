using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
public class KeyCodeController : MonoBehaviour
{
    //�ػ�ť
    // �����¼���
    private const int WH_KEYBOARD_LL = 13;
    // ���̰��� �¼���
    private const int WM_KEYDOWN = 0x0100;
    // ����̧�� �¼���
    private const int WM_KEYUP = 0x0101;
    private const int WM_SYSKEYDOWN = 0x0104;
    private const int WM_SYSKEYUP = 0x0105;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    /// �źŴ洢�ֵ�
    /// ��Ϊ�ڼ��̴��ڱ����µ�״̬��ϵͳ��һֱ���� ���̰��µ� hook
    /// ������ϣ��ֻ�ڼ��̱����µ�һ˲�䣬����һ�λص�
    /// �����ڼ��̱�����ʱ �洢һ��Ϊ true ���ź�
    /// �ڵ��ûص�ǰ�ж϶�Ӧ�����źţ����Ϊ true �򲻵��ã�Ϊ false ����ã������� �ź�תΪ true
    /// �ڰ�����̧��ʱ�����ź����û� false
    private static Dictionary<KeyCode, bool> keyDownDic = new Dictionary<KeyCode, bool>();
    // Use this for initialization
    void Start()
    {
        // ������¼� hook ע��ص�
        _hookID = SetHook(_proc);
    }
    void OnApplicationQuit()
    {
        // ע�� hook �ص�
        UnhookWindowsHookEx(_hookID);
    }
    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    // �����¼� hook �ص�
    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        // �����ǰ�а���������
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam); // ���µİ����ļ�����
            //UnityEngine.Debug.LogError($"vkCode{vkCode}");

            if(vkCode == 9)
            {
                vkCode -= 32;
                //UnityEngine.Debug.LogError($"����tab{vkCode}");
            }

            KeyCode key = KeyCode.None;
            try
            {
                // �󲿷ְ����ļ����� �� Unity �� KeyCokde ת�������� KeyCode key = (vkCode + 32)
                // ������Щ���ܣ����� �س�����TAB���ȣ����Ϊ�˷�ֹ�����������Ҫ�� try catch �ó����ȶ�
                key = (KeyCode)(vkCode + 32);
                
            }
            catch { }
            // �ж��Ƿ��ǰ��°�����˲��
            if (!keyDownDic.ContainsKey(key) || keyDownDic[key] == false)
            {
                // ͨ�� HookInput ������Ϸע��İ����ص�
                //UnityEngine.Debug.LogError($"����tab��˲��");
                HookInput.Single.KeyDownCallBack(key);
                // �ֵ�����д������ֵ�û�д洢 key �����ֵ�����Ĭ�ϵ��� dic.Add() ����
                // �������ź���Ϊ true
                keyDownDic[key] = true;
            }
        }
        // ��������б�̧��
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            if (vkCode == 9)
            {
                vkCode -= 32;
            }
            KeyCode key = KeyCode.None;
            try
            {
                key = (KeyCode)(vkCode + 32);
            }
            catch { }
            // �������źŻָ�Ϊ false
            keyDownDic[key] = false;
            // ͨ�� HookInput ������Ϸע��İ����ص�
            HookInput.Single.KeyUpCallBack(key);
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}