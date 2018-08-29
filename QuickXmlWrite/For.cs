using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static class XmlWrite
    {
        public static XmlWriter<XmlWriterNode<TInput>> For<TInput>()
        {
            return Result<TInput>.WriterNodeResultFromState;
        }

        public static XmlWriter<XmlWriterNode<object>> ForAny()
        {
            return Result<object>.WriterNodeResultFromState;
        }
    }
}