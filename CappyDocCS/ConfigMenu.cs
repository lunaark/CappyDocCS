using System;
using System.IO;
using System.Windows.Forms;

namespace CappyDocCS
{
    public partial class ConfigMenu : Form
    {
        private static string cfgContent = File.ReadAllText(Cappy.CfgPath);
        private static string[] cfgValues = cfgContent.Split(Cappy.Seperator[0]);

        // indexes of all paths in the CFG file

        private const int indexOutPath = 0;
        private const int indexProjPath = 1;

        public ConfigMenu()
        {
            // initialize form
            InitializeComponent();

            // set text on buttons and textboxes
            txtOutPath.Text = Cappy.FolderName;
            txtProjPath.Text = Cappy.ProjectPath;

            // because project path can be set outside of this form, just be sure that it's saved in the config.
            cfgValues[indexProjPath] = Cappy.ProjectPath;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(Cappy.CfgPath))
            {
                writer.BaseStream.Position = 0; // ensure stream is at zero
                writer.BaseStream.SetLength(0);

                for (var i = 0; i < cfgValues.Length; i++)
                {
                    writer.Write(cfgValues[i]); // write that
                    writer.Write(Cappy.Seperator[0]); // shit
                }
            }
            Dispose();
            Close();
        }

        private void btnOutPath_Click(object sender, EventArgs e)
        {
            using (var outPath = new FolderBrowserDialog())
            {
                outPath.Description = "Select the output directory...";
                outPath.SelectedPath = Cappy.FolderName;

                DialogResult result = outPath.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(outPath.SelectedPath))
                {
                    cfgValues[indexOutPath] = outPath.SelectedPath;
                    Cappy.FolderName = outPath.SelectedPath;
                    txtOutPath.Text = outPath.SelectedPath;
                }
            }
        }

        private void btnProjPath_Click(object sender, EventArgs e)
        {
            using (var projPath = new OpenFileDialog())
            {
                projPath.Filter = "Cappy Projects(*.capproj)|*.capproj";
                projPath.Title = "Select your Cappy project...";
                projPath.InitialDirectory = Cappy.ProjectPath;

                DialogResult result = projPath.ShowDialog();

                if (result == DialogResult.OK)
                {
                    cfgValues[indexProjPath] = projPath.FileName;
                    Cappy.ProjectPath = projPath.FileName;
                    txtProjPath.Text = projPath.FileName;
                }
            }
        }
    }
}