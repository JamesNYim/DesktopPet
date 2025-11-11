using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ClickThroughToggler : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")] private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")] private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_TRANSPARENT = 0x00000020;
    private const int WS_EX_LAYERED = 0x00080000;

    private IntPtr hWnd;
    private bool isClickThrough = true;

    void Start()
    {
        hWnd = GetActiveWindow();
        SetClickThrough(true); // start in click-through mode
    }

    void Update()
    {
        // Example trigger — check if mouse over pet or clicked
        if (IsMouseOverPet() && isClickThrough)
        {
            SetClickThrough(false); // disable click-through to interact
        }
        else if (!IsMouseOverPet() && !isClickThrough)
        {
            SetClickThrough(true); // re-enable transparency
        }
    }

    void SetClickThrough(bool enable)
    {
        int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

        if (enable)
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_TRANSPARENT | WS_EX_LAYERED);
        else
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle & ~WS_EX_TRANSPARENT);

        isClickThrough = enable;
    }

    bool IsMouseOverPet()
    {
        // Raycast from mouse position to pet collider
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Pet");
    }
#endif
}
