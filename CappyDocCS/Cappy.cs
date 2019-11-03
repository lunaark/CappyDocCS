using System;
using System.Globalization;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CappyDocCS
{
    public static class Cappy
    {
        private static string cfgPath = @"cfg\cappyConfig.dat";
        private static string folderName;
        private static string templatePath;
        private static string logoPath;
        private static string projectPath;
        private static char[] seperator = { '?' };

        private static string prefix = "CAP";
        private static string fieldSeperator = "-";
        private static string extension = ".bmp";
        private static string projectExtension = ".capproj";
        private static string docExtension = ".doc";

        public static string FieldSeperator
        {
            get
            {
                return fieldSeperator;
            }
        }

        public static string DocExtension
        {
            get
            {
                return docExtension;
            }
        }

        public static string ProjectExtension
        {
            get
            {
                return projectExtension;
            }
        }

        public static string Prefix
        {
            get
            {
                return prefix;
            }
        }

        public static string Extension
        {
            get
            {
                return extension;
            }
        }

        public static string ProjectPath
        {
            get
            {
                return projectPath;
            }
            set
            {
                projectPath = value;
            }
        }

        public static char[] Seperator
        {
            get
            {
                return seperator;
            }
        }

        public static string TemplatePath
        {
            get
            {
                return templatePath;
            }
            set
            {
                templatePath = value;
            }
        }

        public static string LogoPath
        {
            get
            {
                return logoPath;
            }
            set
            {
                logoPath = value;
            }
        }

        public static string CfgPath
        {
            get
            {
                return cfgPath;
            }
            private set
            {
                cfgPath = value;
            }
        }

        public static string FolderName
        {
            get
            {
                return folderName;
            }
            set
            {
                folderName = value;
            }
        }

        public static string GetSaveTime()
        {
            // let there be time
            DateTime SaveTime = DateTime.Now;
            CultureInfo deCulture = new CultureInfo("de-DE");

            string FormattedSaveTime = SaveTime.ToString(deCulture);

            // roslynator says this is a redundant assignment, but strings are immutable
            FormattedSaveTime = FormattedSaveTime.Replace(":", ".").Replace(" ", "-");

            return FormattedSaveTime;
        }

        public static void Config()
        {
            if (File.Exists(CfgPath))
            {
                using (FileStream fs = File.OpenRead(CfgPath))
                {
                    /*
                        *
                        * this code handles parsing and reading the config file.
                        * because the structure of config file is always data then seperator,
                        * you can always split the contents of the file by the seperator
                        * and parse every other value beginning at the zero-index
                        * to get the desired values. we also must check that the config file
                        * has not been tampered with by curious eyes.
                        *
                        * to keep the code cleaner, the index of all the needed values will be hard-coded constants.
                        * here's a legend.
                        *
                        * index 0 - output directory
                        * index 1 - template path
                        * index 2 - logo path
                        *
                        */
                    string cfgContent = File.ReadAllText(CfgPath);
                    string[] cfgValues = cfgContent.Split(Seperator);

                    const int indexOutPath = 0;
                    const int indexTempPath = 1;
                    const int indexLogoPath = 2;
                    const int indexProjPath = 3;

                    for (var i = 0; i < cfgValues.Length; i++)
                    {
                        switch (i)
                        {
                            case indexOutPath:
                                try
                                {
                                    FolderName = cfgValues[indexOutPath];
                                }
                                catch (IOException)
                                {
                                    SystemSounds.Beep.Play();
                                    MessageBox.Show($"Malformed config file, regenerating on next launch!", "Error!");
                                    fs.Dispose();
                                    File.Delete(CfgPath);
                                    System.Environment.Exit(1);
                                }
                                break;

                            case indexTempPath:
                                try
                                {
                                    TemplatePath = cfgValues[indexTempPath];
                                }
                                catch (IOException)
                                {
                                    SystemSounds.Beep.Play();
                                    MessageBox.Show($"Malformed config file, regenerating on next launch!", "Error!");
                                    fs.Dispose();
                                    File.Delete(CfgPath);
                                    System.Environment.Exit(1);
                                }
                                break;

                            case indexLogoPath:
                                try
                                {
                                    LogoPath = cfgValues[indexLogoPath];
                                }
                                catch (IOException)
                                {
                                    SystemSounds.Beep.Play();
                                    MessageBox.Show($"Malformed config file, regenerating on next launch!", "Error!");
                                    fs.Dispose();
                                    File.Delete(CfgPath);
                                    System.Environment.Exit(1);
                                }
                                break;

                            case indexProjPath:
                                try
                                {
                                    ProjectPath = cfgValues[indexProjPath];
                                }
                                catch (IOException)
                                {
                                    SystemSounds.Beep.Play();
                                    MessageBox.Show($"Malformed config file, regenerating on next launch!", "Error!");
                                    fs.Dispose();
                                    File.Delete(CfgPath);
                                    System.Environment.Exit(1);
                                }
                                break;
                        }
                    }
                    fs.Dispose();
                }
            }
            else
            {
                File.Create(CfgPath).Dispose();

                using (StreamWriter sw = new StreamWriter(CfgPath))
                {
                    const int indexOutPath = 0;
                    const int indexTempPath = 1;
                    const int indexLogoPath = 2;
                    const int indexProjPath = 3;

                    string outPath = Path.Combine(Application.StartupPath, "Projects");
                    string tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Templates\Normal.dotm");
                    string logoPath = Path.Combine(Application.StartupPath, @"res\logo.png");
                    string projPath = Path.Combine(Application.StartupPath, @"Projects\default.capproj");

                    File.Create(projPath).Dispose();

                    for (var i = 0; i < 4; i++)
                    {
                        switch (i)
                        {
                            case indexOutPath:
                                Cappy.FolderName = outPath;
                                sw.Write(outPath);
                                break;

                            case indexTempPath:
                                Cappy.TemplatePath = tempPath;
                                sw.Write(tempPath);
                                break;

                            case indexLogoPath:
                                Cappy.LogoPath = logoPath;
                                sw.Write(logoPath);
                                break;

                            case indexProjPath:
                                Cappy.ProjectPath = projPath;
                                sw.Write(projPath);
                                break;
                        }
                        sw.Write(seperator[0]);
                    }
                }
            }
        }
    }
}