using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static class XmlWrite<TInput>
    {
        //public static XmlWriter<XmlWriterNode<TInput>> Tag(string tag)
        //{
        //    return 
        //        state =>
        //        {
        //            state.AppendTag(tag);
        //            return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(state.Current), state);
        //        };
        //}

        //public static XmlWriter<XmlWriterNode<TInput>> Tag(Func<TInput, string> func)
        //{
        //    return
        //        state =>
        //        {
        //            state.AppendTag(func((TInput)state.CurrentInput));
        //            return Result<TInput>.FromState(state);
        //        };
        //}
    }

    public static partial class XmlWriteExt
    {
        public static XmlWriter<XmlWriterNode<TInput>> Tag<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        {
            return
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node;
                    state.AppendTag(func((TInput)state.CurrentInput));
                    return Result<TInput>.FromState(state);
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
                    return Result<TInput>.FromState(state);
                };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Up<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer)
        {
            return
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node.Parent;
                    return Result<TInput>.FromState(state);
                };
        }
    }
}
