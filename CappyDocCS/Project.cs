using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CappyDocCS
{
    public class Project
    {
        // shamelessly reuse code from DocumentScript
        private List<string> scriptItems = new List<string>();

        private DocumentScript DocScript = new DocumentScript();

        public void AddItem(string item)
        {
            // something something add things
            scriptItems.Add(item);
        }

        public void OpenProject()
        {
            using (var openProj = new OpenFileDialog())
            {
                DialogResult result = openProj.ShowDialog();
                openProj.Filter = "Cappy Projects (*.capproj)|*.capproj";
                if (result == DialogResult.OK)
                {
                    // yeah
                    Cappy.ProjectPath = openProj.FileName;
                }
            }
        }

        public void CreateProject()
        {
            // kinda losing my sanity
            string FullFileName = Path.Combine(Cappy.FolderName, Cappy.Prefix + Cappy.GetSaveTime() + Cappy.ProjectExtension);
            File.Create(FullFileName).Dispose();

            Cappy.ProjectPath = FullFileName;
        }

        public void SaveProject()
        {
            using (StreamWriter sw = new StreamWriter(Cappy.ProjectPath))
            {
                sw.BaseStream.Position = 0; // just to be sure
                for (int i = 0; i < scriptItems.Count; i++)
                {
                    // yadda dee yadda doo, this appends scriptItems to the project file just for you
                    sw.Write(scriptItems[i], true);
                }
            }
        }

        public void BuildProject()
        {
            // instantiate the file for reading
            string projContent = File.ReadAllText(Cappy.ProjectPath);

            // remove the last ? from the file (always the last byte), because it is used to separate scriptItems. leaving this in would be catastrophic, but there's probably a better way.
            projContent = projContent.Remove(projContent.Length - 1);

            // now that there is a ? between each scriptItem, and not at the start or ends, we can properly index them all in an array
            string[] items = projContent.Split('?');

            // iterate through all the split items and add them to the document builder for document construction.
            foreach (string item in items)
            {
                DocScript.AddItem(item);
            }

            // YEAH I LOVE MICROSOFT WORD
            DocScript.Publish();
        }
    }
}