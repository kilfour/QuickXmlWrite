using System;
using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriterNode<TInput> writerNode, string name, string value)
        {
            return state =>
            {
                writerNode.Node.Attributes.Add(name, value);
                return new Result<XmlWriterNode<TInput>>(writerNode, state);
            };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriterNode<TInput> writerNode, string name, Func<TInput, string> func)
        {
            return state =>
            {
                writerNode.Node.Attributes.Add(name, func((TInput)state.CurrentInput));
                return new Result<XmlWriterNode<TInput>>(writerNode, state);
            };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string name, string value)
        {
            return 
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node;
                    state.AppendAttribute(name, value);
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string name, Func<TInput, string> func)
        {
            return
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node;
                    state.AppendAttribute(name, func((TInput)state.CurrentInput));
                    return Result<TInput>.WriterNodeResultFromState(state);
                };

        }
    }
}
