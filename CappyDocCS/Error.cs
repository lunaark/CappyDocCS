using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CappyDocCS
{
    internal static class Error
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