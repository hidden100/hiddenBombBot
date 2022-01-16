using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class MouseClickHelper
    {
        public static void ClickOnImage(string path)
        {
            Point? toClick = ImageHelper.FindImageOnPScreen(path);
            if (toClick != null)
            {
                MoveCursorToPoint(toClick.Value.X, toClick.Value.Y);
                Thread.Sleep(500);
                DoMouseClick();
                Thread.Sleep(500);
            }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void DoMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        public static void MoveCursorToPoint(int x, int y)
        {
            SetCursorPos(x, y);
        }
    }
}
