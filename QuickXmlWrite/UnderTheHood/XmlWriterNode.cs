using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite.UnderTheHood
{
    public class XmlWriterNode<TInput>
    {
        public  Node Node { get; set; }

        public XmlWriterNode(Node node)
        {
            Node = node;
        }
    }
}