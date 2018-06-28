using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite.UnderTheHood
{
    public class State
    {
        public readonly Document Document = new Document();
        public Node Current { get; set; }
        public object CurrentInput { get; set; }
        
        public string AsString()
        {
            return Document.Root.AsString();
        }
    }
}