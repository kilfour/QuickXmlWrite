using System;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExt
    {
        public static XmlWriter<Content, TInput> Content<TInput>(this XmlWriter<Node, TInput> writer, Func<TInput, string> func)
        {
            return
                state =>
                {
                    var result = writer(state);
                    var content = new Content { Text = func((TInput)state.CurrentInput) };
                    result.Value.Children.Add(content);
                    return new Result<Content>(content, state);
                };
        }
    }
}
