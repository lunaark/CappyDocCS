using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

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

            // do something with the window title and other windows functions

            Bitmap FullCapture = ScreenCapture.GetScreenShot(posX, posY, 0);
            FullCapture.Save(FullFileName);
            FullCapture.Dispose();

            Bitmap FocusedCapture = ScreenCapture.GetScreenShot(posX, posY, 200);
            FocusedCapture.Save(FocusFileName);
            FocusedCapture.Dispose();

            string CaptureDetails = buttonClicked + ";" + WindowText + ";" + FullFileName + ";" + FocusFileName + "?";
            return CaptureDetails;
        }

        public static string GetCapture(string buttonClicked)
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

            Debug.WriteLine("Button clicked: " + buttonClicked);

            // rectangl shid
            Rectangle bounds = new Rectangle();
            NativeMethods.RECT rct;
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            NativeMethods.GetWindowRect(hwnd, out rct);

            bounds.X = rct.Left - 16; // ditto
            bounds.Y = rct.Top - 16; // dittoo
            bounds.Width = (rct.Right - rct.Left) + 32; // add 32, because you want to account for 16 pixels on both sides
            bounds.Height = (rct.Bottom - rct.Top) + 32; // dittooo

            // do something with the window title and other windows functions

            Bitmap FullCapture = ScreenCapture.GetScreenShot(bounds.X, bounds.Y, 0);
            FullCapture.Save(FullFileName);
            FullCapture.Dispose();

            string CaptureDetails = buttonClicked + ";" + FullFileName + "?";
            return CaptureDetails;
        }
    }
}