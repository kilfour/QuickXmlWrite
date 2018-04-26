using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite.UnderTheHood
{
    public class State
    {
        private readonly Document document = new Document();
        public Item Current { get; set; }
        public object CurrentInput { get; set; }

        public void AppendTag(string tag)
        {
            if (document.Root == null)
            {
                document.Root = new Node { Name = tag };
                Current = document.Root;
            } 
        }

        public string AsString()
        {
            return document.Root.AsString();
        }

        //public void AppendContent(string content)
        //{
        //    ((Node) Current).Children.Add(new Content {Text = content});
        //}
    }
}