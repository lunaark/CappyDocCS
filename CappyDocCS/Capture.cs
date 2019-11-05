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
            string buttonAction = String.Empty;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            FullFileName = folder + Cappy.Prefix + Cappy.FieldSeperator + saveTime + Cappy.FieldSeperator + "full" + Cappy.Extension;
            FocusFileName = folder + Cappy.Prefix + Cappy.FieldSeperator + saveTime + Cappy.FieldSeperator + "focus" + Cappy.Extension;

            IntPtr hwnd = NativeMethods.WindowFromPoint(posX, posY);
            string WindowText = NativeMethods.GetWindowTextByWM(hwnd);

            Bitmap FullCapture = ScreenCapture.GetScreenShot(posX, posY, 0, false);
            FullCapture.Save(FullFileName);
            FullCapture.Dispose();

            Bitmap FocusedCapture = ScreenCapture.GetScreenShot(posX, posY, 1, false);
            FocusedCapture.Save(FocusFileName);
            FocusedCapture.Dispose();

            if (buttonClicked.Equals("Left"))
            {
                buttonAction = "Click";
            }
            else if (buttonClicked.Equals("Right"))
            {
                buttonAction = "Right-click";
            }
            else if (buttonClicked.Equals("Tab") ||
                     buttonClicked.Equals("Escape") ||
                     buttonClicked.Equals("Enter"))
            {
                buttonAction = "Press";
            }

            if(String.IsNullOrEmpty(WindowText))
            {
                WindowText = "<< UNKNOWN >>";
            }

            string CaptureDetails = buttonAction + ";" + buttonClicked + ";" + WindowText + ";" + FullFileName + ";" + FocusFileName + "?";
            return CaptureDetails;
        }

        public static string GetCapture(string buttonClicked)
        {
            // method overloading is cool
            string saveTime = Cappy.GetSaveTime();
            string folder = Cappy.FolderName + @"\Images\";

            string FileName;

            string buttonAction = String.Empty;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            FileName = folder + Cappy.Prefix + Cappy.FieldSeperator + saveTime + Cappy.FieldSeperator + "key" + Cappy.Extension;

            // rectangl shid
            Rectangle bounds = new Rectangle();
            NativeMethods.RECT rct;
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            IntPtr parentHwnd = NativeMethods.GetParent(hwnd);
            if (parentHwnd == IntPtr.Zero)
            {
                // if foreground window is already top level, just use it's hwnd
                NativeMethods.GetWindowRect(hwnd, out rct);

                bounds.X = rct.Left;
                bounds.Y = rct.Top;
                bounds.Width = rct.Right - rct.Left;
                bounds.Height = rct.Bottom - rct.Top;
            }
            else
            {
                // otherwise, get the top level window in the foreground window's hierarchy
                NativeMethods.GetWindowRect(parentHwnd, out rct);

                bounds.X = rct.Left;
                bounds.Y = rct.Top;
                bounds.Width = rct.Right - rct.Left;
                bounds.Height = rct.Bottom - rct.Top;
            }

            Bitmap Capture = ScreenCapture.GetScreenShot(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2), 1, true);
            Capture.Save(FileName);
            Capture.Dispose();

            if (buttonClicked.Equals("Left"))
            {
                buttonAction = "Click";
            }
            else if (buttonClicked.Equals("Right"))
            {
                buttonAction = "Right-click";
            }
            else if (buttonClicked.Equals("Tab") ||
                     buttonClicked.Equals("Escape") ||
                     buttonClicked.Equals("Enter"))
            {
                buttonAction = "Press";
            }

            string CaptureDetails = buttonAction + ";" + buttonClicked + ";" + FileName + "?";
            return CaptureDetails;
        }
    }
}