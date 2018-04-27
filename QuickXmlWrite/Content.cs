using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExt
    {
        public static XmlWriter<Content> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        {
            return
                state =>
                {
                    var result = writer(state);
                    var content = new Content { Text = func((TInput)state.CurrentInput) };
                    result.Value.Node.Children.Add(content);
                    return new Result<Content>(content, state);
                };
        }
    }
}
