using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriterNode<TInput> writerNode, string text, bool asCData = false)
        {
            return state => {
                var content = new Content { Text = DecorateWithCDataIfRequired(text, asCData) };
                writerNode.Node.Add(content);
                return new Result<XmlWriterNode<TInput>>(writerNode, state); };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriterNode<TInput> writerNode, Func<TInput, string> func, bool asCData = false)
        {
            return state => {
                var content = new Content { Text = DecorateWithCDataIfRequired(state.GetValue(func), asCData) };
                writerNode.Node.Add(content);
                return new Result<XmlWriterNode<TInput>>(writerNode, state); };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string value, bool asCData = false)
        {
            return state => {
                var result = writer(state);
                var content = new Content {Text = DecorateWithCDataIfRequired(value, asCData) };
                result.Value.Node.Add(content);
                return Result<TInput>.WriterNodeResultFromState(state); };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Content<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func, bool asCData = false)
        {
            return state => {
                var result = writer(state);
                var content = new Content {Text = DecorateWithCDataIfRequired(state.GetValue(func), asCData) };
                result.Value.Node.Add(content);
                return Result<TInput>.WriterNodeResultFromState(state); };
        }

        private static string DecorateWithCDataIfRequired(string text, bool asCData)
        {
            if (asCData)
            {
                return $"<![CDATA[{text}]]>";
            }
            return text;
        }
    }
}
