using System.Collections.Generic;
using System.Threading.Tasks;

namespace CappyDocCS
{
    public class DocumentScript
    {
        private List<string> scriptItems = new List<string>();

        public void AddItem(string CaptureDetails)
        {
            scriptItems.Add(CaptureDetails);
        }

        public void Publish()
        {
            DocumentBuilder Document = new DocumentBuilder();
            Document.BuildDocument(scriptItems.ToArray());
        }
    }
}