using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        private static void AppendTag(State state, string tag)
        {
            var node = new Node { Name = tag };
            if (state.Document.Root == null)
            {
                state.Document.Root = node;
            }
            else
            {
                state.Current.Add(node);
            }
            state.Current = node;
        }

        private static IResult<XmlWriterNode<TInput>> AppendTag<TInput>( State state, XmlWriterNode<TInput> writerNode, string tag)
        {
            if (writerNode.Node == null)
            {
                AppendTag(state, tag);
                return Result<TInput>.WriterNodeResultFromState(state);
            }
            var newnode = new Node { Name = tag };
            writerNode.Node.Add(newnode);
            return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(newnode), state);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriterNode<TInput> writerNode, string tag)
        {
            return state => AppendTag(state, writerNode, tag);
        }
        
        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriterNode<TInput> writerNode, Func<TInput, string> func)
        {
            return state => AppendTag(state, writerNode, func((TInput)state.CurrentInput));
        }

        private static IResult<XmlWriterNode<TInput>> AppendTag<TInput>(State state, XmlWriter<XmlWriterNode<TInput>> writer, string tag)
        {
            var result = writer(state);
            state.Current = result.Value.Node;
            AppendTag(state, tag);
            return Result<TInput>.WriterNodeResultFromState(state);
        }

        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        {
            return state => AppendTag(state, writer, func((TInput)state.CurrentInput));
        }

        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string tag)
        {
            return state => AppendTag(state, writer, tag);
        }
    }
}
