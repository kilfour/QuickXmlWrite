using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriterNode<TInput> writerNode, string text)
        {
            return state => Content(state, writerNode, text);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriterNode<TInput> writerNode, Func<TInput, string> func)
        {
            return state => Content(state, writerNode, state.GetValue(func));
        }

        private static IResult<XmlWriterNode<TInput>> Content<TInput>(State state, XmlWriterNode<TInput> writerNode, string text)
        {
            var content = new Content { Text = text };
            writerNode.Node.Add(content);
            return new Result<XmlWriterNode<TInput>>(writerNode, state);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string value)
        {
            return state => Content(state, writer, value);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        {
            return state => Content(state, writer, state.GetValue(func));
        }

        private static IResult<XmlWriterNode<TInput>> Content<TInput>(State state, XmlWriter<XmlWriterNode<TInput>> writer, string value)
        {
            var result = writer(state);
            var content = new Content {Text = value};
            result.Value.Node.Add(content);
            return Result<TInput>.WriterNodeResultFromState(state);
        }
    }
}
