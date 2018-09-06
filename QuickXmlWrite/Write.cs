using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static string Write<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, TInput input)
        {
            return writer(new State {CurrentInput = input}).AsString();
        }

        public static string WriteHumanReadable<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, TInput input)
        {
            return writer(new State { CurrentInput = input }).AsHumanReadableString();
        }
    }
}