using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowClickControl : MonoBehaviour
{
    #if UNITY_STANDALONE_WIN
        const int GWL_EXSTYLE = -20;
        const int WS_EX_TRANSPARENT = 0x20;
        const int WS_EX_LAYERED = 0x80000;

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private IntPtr hwnd;
        private bool isClickThrough = true;

        void Start()
        {
            hwnd = GetActiveWindow();
            SetClickThrough(true); // start as click-through
        }

        public void SetClickThrough(bool enable)
        {
            int style = GetWindowLong(hwnd, GWL_EXSTYLE);

            if (enable)
                style |= WS_EX_TRANSPARENT;
            else
                style &= ~WS_EX_TRANSPARENT;

            SetWindowLong(hwnd, GWL_EXSTYLE, style);
            isClickThrough = enable;
            Debug.Log($"Click-through set to {enable}");
        }

        // Optional: toggle mode with right-click
        void Update()
        {
            if (Input.GetMouseButtonDown(1)) // right-click toggles
            {
                SetClickThrough(!isClickThrough);
            }
        }
    #endif
}
