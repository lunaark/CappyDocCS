using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace CappyDocCS
{
    public static class Capture
    {
        public static string GetCapture(int posX, int posY, string buttonClicked)
        {
            string saveTime = Cappy.GetSaveTime();
            string folder = Cappy.FolderName + @"\Images\";

            string FullFileName = String.Empty;
            string FocusFileName = String.Empty;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            FullFileName = folder + Cappy.Prefix + Cappy.FieldSeperator + saveTime + Cappy.FieldSeperator + "full" + Cappy.Extension;
            FocusFileName = folder + Cappy.Prefix + Cappy.FieldSeperator + saveTime + Cappy.FieldSeperator + "focus" + Cappy.Extension;

            Debug.WriteLine("Button clicked: " + buttonClicked);

            IntPtr hwnd = NativeMethods.WindowFromPoint(posX, posY);
            string WindowText = NativeMethods.GetWindowTextByWM(hwnd);

            Bitmap FullCapture = ScreenCapture.GetScreenShot(posX, posY, 0);
            FullCapture.Save(FullFileName);
            FullCapture.Dispose();

            Bitmap FocusedCapture = ScreenCapture.GetScreenShot(posX, posY, 1);
            FocusedCapture.Save(FocusFileName);
            FocusedCapture.Dispose();

            string CaptureDetails = buttonClicked + ";" + WindowText + ";" + FullFileName + ";" + FocusFileName + "?";
            return CaptureDetails;
        }

        public static string GetCapture(string buttonClicked)
        {
            // method overloading is cool
            string saveTime = Cappy.GetSaveTime();
            string folder = Cappy.FolderName + @"\Images\";

            string FileName;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            FileName = folder + Cappy.Prefix + Cappy.FieldSeperator + saveTime + Cappy.FieldSeperator + "key" + Cappy.Extension;

            Debug.WriteLine("Button clicked: " + buttonClicked);

            // rectangl shid
            Rectangle bounds = new Rectangle();
            NativeMethods.RECT rct;
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            NativeMethods.GetWindowRect(hwnd, out rct);

            bounds.X = rct.Left;
            bounds.Y = rct.Top;
            bounds.Width = rct.Right - rct.Left;
            bounds.Height = rct.Bottom - rct.Top;

            Bitmap Capture = ScreenCapture.GetScreenShot(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2), 1);
            Capture.Save(FileName);
            Capture.Dispose();

            string CaptureDetails = buttonClicked + ";" + FileName + "?";
            return CaptureDetails;
        }
    }
}