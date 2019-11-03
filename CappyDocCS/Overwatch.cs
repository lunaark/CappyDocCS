using System.Threading.Tasks;
using System.Windows.Forms;

using Gma.System.MouseKeyHook;

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
            m_GlobalHook = Hook.GlobalEvents();

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

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case (char)Keys.Tab:
                    Capture.GetCapture(e.KeyChar.ToString());
                    break;
                case (char)Keys.Escape:
                    Capture.GetCapture(e.KeyChar.ToString());
                    break;
                case (char)Keys.Enter:
                    Capture.GetCapture(e.KeyChar.ToString());
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