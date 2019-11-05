using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace CappyDocCS
{
    public static class Cappy
    {
        private static string cfgPath = @"cfg\cappyConfig.dat";
        private static string folderName;
        private static string projectPath;
        private static bool isRecording = false;
        private static char[] seperator = { '?' };

        private static string prefix = "CAP";
        private static string fieldSeperator = "-";
        private static string extension = ".bmp";
        private static string projectExtension = ".capproj";
        private static string docExtension = ".doc";

        public static bool IsRecording
        {
            get
            {
                return isRecording;
            }
            set
            {
                isRecording = value;
            }
        }

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
                    const int indexProjPath = 1;

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
                                    fs.Dispose();
                                    Error.invalidCfg();
                                }
                                break;

                            case indexProjPath:
                                try
                                {
                                    ProjectPath = cfgValues[indexProjPath];
                                }
                                catch (IOException)
                                {
                                    fs.Dispose();
                                    Error.invalidCfg();
                                }
                                break;
                        }
                    }
                    fs.Dispose();
                }
            }
            else
            {
                if (!Directory.Exists(Path.GetDirectoryName(CfgPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(CfgPath));
                }
                File.Create(CfgPath).Dispose();

                using (StreamWriter sw = new StreamWriter(CfgPath))
                {
                    const int indexOutPath = 0;
                    const int indexProjPath = 1;

                    string outPath = Path.Combine(Application.StartupPath, "Projects");
                    string projPath = Path.Combine(Application.StartupPath, @"Projects\default.capproj");

                    if (!Directory.Exists(Path.GetDirectoryName(projPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(projPath));
                    }
                    File.Create(projPath).Dispose();

                    for (var i = 0; i < 4; i++)
                    {
                        switch (i)
                        {
                            case indexOutPath:
                                Cappy.FolderName = outPath;
                                sw.Write(outPath);
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