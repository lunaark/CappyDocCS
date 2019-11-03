using System;
using System.Drawing;
using System.Windows.Forms;

namespace CappyDocCS
{
    public static class ScreenCapture
    {
        public static Bitmap GetScreenShot(int posX, int posY, int mode)
        {
            /*
             *
             *  Method Documentation
             *
             *  This method quite simply, gets a screenshot.
             *
             */
            Rectangle bounds = new Rectangle();
            Rectangle winBounds = new Rectangle();

            const int rectHeight = 20;
            const int rectWidth = 20;
            int rectX = posX - (rectWidth / 2);
            int rectY = posY - (rectHeight / 2);

            if (mode == 0)
            {
                // full-screen capture
                bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                // window capture
                NativeMethods.RECT rct;

                IntPtr handle = NativeMethods.WindowFromPoint(posX, posY);
                IntPtr parentHandle = NativeMethods.GetParent(handle);

                if (parentHandle == IntPtr.Zero)
                {
                    // if the window is already top-level, just use windowfrompoint, as parentHandle will be null
                    NativeMethods.GetWindowRect(handle, out rct);
                }
                else
                {
                    // otherwise, use the top-level window in that window's hierarchy.
                    NativeMethods.GetWindowRect(parentHandle, out rct);
                }

                // make the bounds whatever is in RCT, then zoom out by 6 pixels
                bounds.X = rct.Left - 16; // ditto
                bounds.Y = rct.Top - 16; // dittoo
                bounds.Width = (rct.Right - rct.Left) + 32; // add 32, because you want to account for 16 pixels on both sides
                bounds.Height = (rct.Bottom - rct.Top) + 32; // dittooo
            }

            // instantiate bitmap
            var clickZoneBitmap = new Bitmap(bounds.Width, bounds.Height);
            clickZoneBitmap.SetResolution(1200, 1200);

            // create image for usage with System.Drawing methods
            var ClickZoneImage = Graphics.FromImage(clickZoneBitmap);

            // pens! PENS!
            Pen redPen = new Pen(Color.Crimson, 2);
            Pen redHighlight = new Pen(Color.Red, 4);

            if (mode == 0)
            {
                NativeMethods.RECT rct;

                IntPtr handle = NativeMethods.WindowFromPoint(posX, posY);

                NativeMethods.GetWindowRect(handle, out rct);

                winBounds.X = rct.Left - 8;
                winBounds.Y = rct.Top - 8;
                winBounds.Width = (rct.Right - rct.Left) + 16;
                winBounds.Height = (rct.Bottom - rct.Top) + 16;

                ClickZoneImage.CopyFromScreen(new Point(0, 0), Point.Empty, bounds.Size);
                ClickZoneImage.DrawRectangle(redPen, rectX, rectY, rectWidth, rectHeight);
                ClickZoneImage.DrawRectangle(redHighlight, winBounds.X, winBounds.Y, winBounds.Width, winBounds.Height);
            }
            else
            {
                /*
                    * Because it was one of the main obstacles in this project, I'd like to explain how I got accurate highlighting in focused screenshots.
                    *
                    * So, I used Console.WriteLine to compare the boundaries of 'bounds' with the attempted corrected value of posX.
                    * I was looking for patterns that would indicate the causality of the inaccuracy.
                    *
                    * I noticed that everything was roughly offset by the position of the window.
                    *
                    * So, I simply took the mouse's position on both axes, subtracted half of the rectangles size on that axe (as rectangles in C# are top-left)
                    *
                    * Then, I simply subtracted the X and Y position of the boundaries, as they're relative to the top-left's distance from the screen's 0,0 coordinates.
                    * This essentially put the window at 0,0, meaning that if you took the mouse cursor's position relative to our virtual 0,0, and had it wrap around if it was greater than
                    * our virtual boxes boundaries using the modulus operator, you will have a 1:1 accurate representation of the cursor's position.
                    *
                    */
                ClickZoneImage.CopyFromScreen(new Point(bounds.X, bounds.Y), Point.Empty, bounds.Size);
                ClickZoneImage.DrawRectangle(redPen, (posX - (rectWidth / 2) - bounds.X) % bounds.Width, (posY - (rectHeight / 2) - bounds.Y) % bounds.Height, rectWidth, rectHeight);
            }
            ClickZoneImage.Dispose();
            return clickZoneBitmap;
        }
    }
}