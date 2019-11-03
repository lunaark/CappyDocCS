using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CappyDocCS
{
    public static class NativeMethods
    {
        /*
            [DllImport("user32.dll")]
            static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int count);

            [DllImport("user32.dll")]
            static extern int InternalGetWindowText(IntPtr hwnd, StringBuilder text, int count);
        */

        /*
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        extern static IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, [In] string lpClassName, [In] string lpWindowName);
        */

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessageGetTextLength(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessageGetText(IntPtr hwnd, int msg, IntPtr wParam, [Out] StringBuilder lParam);

        [DllImport("user32.dll")]
        static public extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static public extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static public extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static public extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        private const int WM_GETTEXT = 0x000D;
        private const int WM_GETTEXTLENGTH = 0x000E;

        public static string GetWindowTextByWM(IntPtr hwnd)
        {
            int length = SendMessageGetTextLength(hwnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);

            if (length > 0 && length < int.MaxValue)
            {
                length++; // room for EOS terminator
                StringBuilder sb = new StringBuilder(length);
                SendMessageGetText(hwnd, WM_GETTEXT, (IntPtr)sb.Capacity, sb);
                return sb.ToString();
            }
            return String.Empty;
        }
    }
}