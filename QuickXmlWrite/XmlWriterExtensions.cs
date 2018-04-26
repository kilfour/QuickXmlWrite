using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static class XmlWriterExtensions
    {
        public static string Write<T, TInput>(this XmlWriter<T> writer, TInput input)
        {
            var state = new State();
            state.CurrentInput = input;
            var result = writer(state);
            return result.AsString();
        }
    }
}