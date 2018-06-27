using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExt
    {
        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        {
            return
                state =>
                {
                    var result = writer(state);
                    var content = new Content { Text = func((TInput)state.CurrentInput) };
                    result.Value.Node.Add(content);
                    return Result<TInput>.FromState(state);
                };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string value)
        {
            return
                state =>
                {
                    var result = writer(state);
                    var content = new Content { Text = value };
                    result.Value.Node.Add(content);
                    return Result<TInput>.FromState(state);
                };
        }
    }
}
