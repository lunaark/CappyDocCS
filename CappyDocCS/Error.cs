using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CappyDocCS
{
    static class Error
    {
        public static void invalidCfg()
        {
            SystemSounds.Beep.Play();
            MessageBox.Show($"Malformed config file, regenerating on next launch!", "Error!");
            File.Delete(Cappy.CfgPath);
            System.Environment.Exit(1);
        }
    }
}
