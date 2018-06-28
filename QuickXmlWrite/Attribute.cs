using System;
using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriterNode<TInput> writerNode, string name, string value)
        {
            return state => Attribute(state, writerNode, name, value);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriterNode<TInput> writerNode, string name, Func<TInput, string> func)
        {
            return state => Attribute(state, writerNode, name, state.GetValue(func));
        }

        private static IResult<XmlWriterNode<TInput>> Attribute<TInput>(State state, XmlWriterNode<TInput> writerNode, string name, string value)
        {
            writerNode.Node.Attributes.Add(name, value);
            return new Result<XmlWriterNode<TInput>>(writerNode, state);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string name, string value)
        {
            return state => Attribute(state, writer, name, value);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string name, Func<TInput, string> func)
        {
            return state => Attribute(state, writer, name, state.GetValue(func));
        }

        private static IResult<XmlWriterNode<TInput>> Attribute<TInput>(State state, XmlWriter<XmlWriterNode<TInput>> writer, string name, string value)
        {
            var result = writer(state);
            state.Current = result.Value.Node;
            state.Current.Attributes.Add(name, value);
            return Result<TInput>.WriterNodeResultFromState(state);
        }
    }
}
