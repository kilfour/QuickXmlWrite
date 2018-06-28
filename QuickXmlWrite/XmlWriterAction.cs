using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public delegate XmlWriter<XmlWriterNode<TInput>> XmlWriterAction<TInput>(XmlWriter<XmlWriterNode<TInput>> writer);
}