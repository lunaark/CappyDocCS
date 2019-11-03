using Gma.System.MouseKeyHook;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CappyDocCS
{
    public class Overwatch
    {
        private Project ProjectObj = new Project();

        private IKeyboardMouseEvents m_GlobalHook;

        private int mouseCoordsX;
        private int mouseCoordsY;

        public void Subscribe()
        {
            var map = new Dictionary<Combination, Action>
            {
                { Combination.FromString("Alt+P"), () => ToggleRecord() },
            };

            m_GlobalHook = Hook.GlobalEvents();
            Hook.GlobalEvents().OnCombination(map);

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            m_GlobalHook.Dispose();
        }

        public void Start()
        {
            Subscribe();
        }

        public void Stop()
        {
            Unsubscribe();
            ProjectObj.SaveProject();
        }

        public void ToggleRecord()
        {
            if (!Cappy.IsRecording)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Tab:
                    Capture.GetCapture("Tab");
                    break;

                case (char)Keys.Escape:
                    Capture.GetCapture("Escape");
                    break;

                case (char)Keys.Enter:
                    Capture.GetCapture("Enter");
                    break;
            }
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            mouseCoordsX = e.X;
            mouseCoordsY = e.Y;
            string CaptureDetails = Capture.GetCapture(e.X, e.Y, e.Button.ToString());
            ProjectObj.AddItem(CaptureDetails);
        }
    }
}