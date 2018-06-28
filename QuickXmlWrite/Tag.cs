using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriterNode<TInput> writerNode, string tag)
        {
            return state =>
            {
                if (writerNode.Node == null)
                {
                    state.AppendTag(tag);
                    return Result<TInput>.WriterNodeResultFromState(state);
                }
                var newnode = new Node { Name = tag };
                writerNode.Node.Add(newnode);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(newnode), state);
            };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriterNode<TInput> writerNode, Func<TInput, string> func)
        {
            return state =>
            {
                if (writerNode.Node == null)
                {
                    state.AppendTag(func((TInput)state.CurrentInput));
                    return Result<TInput>.WriterNodeResultFromState(state);
                }
                var newnode = new Node { Name = func((TInput)state.CurrentInput) };
                writerNode.Node.Add(newnode);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(newnode), state);
            };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        {
            return
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node;
                    state.AppendTag(func((TInput)state.CurrentInput));
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string tag)
        {
            return
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node;
                    state.AppendTag(tag);
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }
    }
}
