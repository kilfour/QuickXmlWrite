using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriterNode<TInput> writerNode, string text)
        {
            return state => {
                var content = new Content { Text = text };
                writerNode.Node.Add(content);
                return (IResult<XmlWriterNode<TInput>>) new Result<XmlWriterNode<TInput>>(writerNode, state); };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriterNode<TInput> writerNode, Func<TInput, string> func)
        {
            return state => {
                var content = new Content { Text = state.GetValue(func) };
                writerNode.Node.Add(content);
                return (IResult<XmlWriterNode<TInput>>) new Result<XmlWriterNode<TInput>>(writerNode, state); };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string value)
        {
            return state => {
                var result = writer(state);
                var content = new Content {Text = value};
                result.Value.Node.Add(content);
                return (IResult<XmlWriterNode<TInput>>) Result<TInput>.WriterNodeResultFromState(state); };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        {
            return state => {
                var result = writer(state);
                var content = new Content {Text = state.GetValue(func)};
                result.Value.Node.Add(content);
                return (IResult<XmlWriterNode<TInput>>) Result<TInput>.WriterNodeResultFromState(state); };
        }
    }
}
