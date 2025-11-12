using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_EX_TRANSPARENT = 0x00000020;
    private const uint LWA_COLORKEY = 0x00000001;

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOACTIVATE = 0x0010;

    private const int GWL_STYLE = -16;
    private const uint WS_CAPTION = 0x00C00000; // Title bar + border
    private const uint WS_THICKFRAME = 0x00040000; // Resizable border

    void Start()
    {
        var hwnd = GetActiveWindow();

        int style = GetWindowLong(hwnd, GWL_STYLE);
        style &= ~(int)(WS_CAPTION | WS_THICKFRAME); // Remove title bar and border

        // No border
        SetWindowLong(hwnd, GWL_STYLE, style);

        // window layered + transparent
        int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);

        // Magenta will be transparent
        SetLayeredWindowAttributes(hwnd, 0, 0, LWA_COLORKEY);

        // Set window always on top
        SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, 0);

    }
#endif
}

