using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
// ���ڱ�ʾ���λ�õĽ�
[StructLayout(LayoutKind.Sequential)]
public class POINT
{
    public int x;
    public int y;
}
// ���ڱ�ʾ����ƶ���Ϣ�Ľṹ��
[StructLayout(LayoutKind.Sequential)]
public class MouseHookStruct
{
    public POINT pt;
    public int hwnd;
    public int wHitTestCode;
    public int dwExtraInfo;
}
public class MouseController : MonoBehaviour
{
    //�ػ�ť
    // ����¼���
    private const int WH_KEYBOARD_LL = 14;
    // ����ƶ��¼���
    private const int WM_MOUSEMOVE = 0x0200;
    // ����Ҽ������¼���
    private const int WM_RBUTTONDOWN = 0x0204;
    // ������̧���¼���
    private const int WM_RBUTTONUP = 0x0205;
    // �����������¼���
    private const int WM_LBUTTONDOWN = 0x0201;
    // ������̧���¼���
    private const int WM_LBUTTONUP = 0x0202;
    private static LowLevelMouseProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    // �洢��һ֡����λ��
    private static Vector3 lastMousePos = Vector3.zero;

    // �洢��ǰ֡����λ��
    public static Vector3 MousePos = Vector3.zero;



    // ��һ֡����ǰ֡�����ƶ���
    public static Vector3 mouseDeltMove = Vector3.zero;
    // �����жϵ�ǰ֡�Ƿ�����Ϸ������ĵ�һ֡
    private static bool first = true;
    // Use this for initialization
    void Start()
    {
        // ע�� hook
        _hookID = SetHook(_proc);
    }
    void OnApplicationQuit()
    {
        // ע�� hook
        UnhookWindowsHookEx(_hookID);
    }
    private static IntPtr SetHook(LowLevelMouseProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
    // hook �ص�
    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        // ֻҪ������¼����������� hook �ص������ͻᱻϵͳ����
        // ����ֱ���ڸ÷����� ͨ�� HookInput ���� ��Ϸע�������¼��ص�
        HookInput.Single.MouseEventCallBack();
        // ���������������
        if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            // ͨ�� HookInput ����������������ʱ�Ļص�
            HookInput.Single.MouseClickCallBack();
        }
        // �������Ҽ�������
        if (nCode >= 0 && wParam == (IntPtr)WM_RBUTTONDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            HookInput.Single.MouseClickCallBack();
        }
        // �����������̧��
        if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONUP)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            HookInput.Single.MouseReleaseCallBack();
        }
        // �������Ҽ���̧��
        if (nCode >= 0 && wParam == (IntPtr)WM_RBUTTONUP)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            HookInput.Single.MouseReleaseCallBack();
        }
        // ������귢�����ƶ�, �������λ��
        if (nCode >= 0 && wParam == (IntPtr)WM_MOUSEMOVE)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            // ��ȡ����ƶ���Ϣ
            MouseHookStruct M_MouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
            // ��������ƶ���Ϣ��ȡ ��ǰ֡ ���λ��
            Vector3 curMousePos = new Vector3(-M_MouseHookStruct.pt.x, M_MouseHookStruct.pt.y, 0f);
            MousePos = curMousePos;
            // �����ǰ����Ϸ������ĵ�һ֡
            if (first)
            {
                // �� lastMousePos = curMousePos
                // �� ��Ϊ��ǰ�����ƶ���Ϊ 0
                lastMousePos = curMousePos;
                first = false;
            }
            // ������һ֡����ǰ֡�����ƶ���
            mouseDeltMove = lastMousePos - curMousePos;
            // ͨ�� HookInput ������Ϸ������ƶ��ص�
            //HookInput.Single.MouseMoveCallBack(mouseDeltMove);

            // ͨ�� HookInput ������Ϸ�����λ�ûص�
            HookInput.Single.MouseMoveCallBack(curMousePos);

            // ���� lastMousePos
            lastMousePos = curMousePos;
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}