using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite.UnderTheHood
{
    public class State
    {
        private readonly Document document = new Document();
        public Node Current { get; set; }
        public object CurrentInput { get; set; }

        public void AppendTag(string tag)
        {
            var node = new Node {Name = tag};
            if (document.Root == null)
            {
                document.Root = node;
            }
            else
            {
                Current.Children.Add(node);
            }
            Current = node;
        }

        public string AsString()
        {
            return document.Root.AsString();
        }
    }
}