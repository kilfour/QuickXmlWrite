using System;
using System.Collections;
using System.Collections.Generic;
using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Many<TInput, TOut>(
            this XmlWriterNode<TInput> writerNode,
            Func<TInput, IEnumerable<TOut>> func,
            XmlWriter<XmlWriterNode<TOut>> innerWriter)
        {
            return
                state =>
                {
                    var oldInput = state.CurrentInput;
                    var oldNode = state.Current;
                    foreach (var element in (IEnumerable)func((TInput)state.CurrentInput))
                    {
                        state.Current = writerNode.Node;
                        state.CurrentInput = element;
                        innerWriter(state);
                        state.Current = oldNode;
                    }
                    state.CurrentInput = oldInput;
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Many<TInput, TOut>(
            this XmlWriterNode<TInput> writerNode,
            Func<TInput, IEnumerable<TOut>> func,
            XmlWriterAction<TOut> innerWriterAction)
        {
            return
                state =>
                {
                    var oldInput = state.CurrentInput;
                    var oldNode = state.Current;
                    foreach (var element in (IEnumerable)func((TInput)state.CurrentInput))
                    {
                        state.Current = writerNode.Node;
                        state.CurrentInput = element;
                        IResult<XmlWriterNode<TOut>> InnerWriter(State innerState) => new Result<XmlWriterNode<TOut>>(new XmlWriterNode<TOut>(innerState.Current), innerState);
                        var appliedWriter = innerWriterAction(InnerWriter);
                        appliedWriter(state);
                        state.Current = oldNode;
                    }
                    state.CurrentInput = oldInput;
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Many<TInput>(this XmlWriter<TInput> writer)
        {
            return
                state =>
                {
                    var oldInput = state.CurrentInput;
                    var oldNode = state.Current;
                    foreach (var element in (IEnumerable)state.CurrentInput)
                    {
                        state.CurrentInput = element;
                        writer(state);
                        state.Current = oldNode;
                    }
                    state.CurrentInput = oldInput;
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }

        //public static XmlWriter<XmlWriterNode<TInput>> Many<TInput, TOut>(
        //    this XmlWriter<XmlWriterNode<TInput>> writer,
        //    Func<TInput, IEnumerable<TOut>> func,
        //    XmlWriter<XmlWriterNode<TInput>> innerWriter)
        //{
        //    return
        //        state =>
        //        {
        //            var oldInput = state.CurrentInput;
        //            var oldNode = state.Current;
        //            var result = writer(state);
        //            foreach (var element in (IEnumerable)func((TInput)state.CurrentInput))
        //            {
        //                state.Current = result.Value.Node;
        //                state.CurrentInput = element;
        //                innerWriter(state);
        //                state.Current = oldNode;
        //            }
        //            state.CurrentInput = oldInput;
        //            return Result<TInput>.WriterNodeResultFromState(state);
        //        };
        //}

        public static XmlWriter<XmlWriterNode<TInput>> Many<TInput, TOut>(
            this XmlWriter<XmlWriterNode<TInput>> writer,
            Func<TInput, IEnumerable<TOut>> func,
            Func<XmlWriter<XmlWriterNode<TOut>>, XmlWriter<XmlWriterNode<TOut>>> innerWriterAction)
        {
            return
                state =>
                {
                    var oldInput = state.CurrentInput;
                    var oldNode = state.Current;
                    var result = writer(state);
                    foreach (var element in (IEnumerable)func((TInput)state.CurrentInput))
                    {
                        state.Current = result.Value.Node;
                        state.CurrentInput = element;
                        IResult<XmlWriterNode<TOut>> InnerWriter(State innerState) => new Result<XmlWriterNode<TOut>>(new XmlWriterNode<TOut>(innerState.Current), innerState);
                        var appliedWriter = innerWriterAction(InnerWriter);
                        appliedWriter(state);
                        state.Current = oldNode;
                    }
                    state.CurrentInput = oldInput;
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }
    }
}
