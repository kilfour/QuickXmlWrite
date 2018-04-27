using System;
using System.Collections;
using System.Collections.Generic;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExt
    {
        public static XmlWriter<object> Many<TInput>(this XmlWriter<TInput> writer)
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
                    return new Result<object>(null, state);
                };
        }

        public static XmlWriter<TOut> ApplyMany<TInput, TOut>(
            this XmlWriterNode<IEnumerable<TInput>> writer, Func<XmlWriterNode<TInput>, XmlWriter<TOut>> func)
        {
            return
                state =>
                {
                    var oldInput = state.CurrentInput;
                    var oldNode = state.Current;
                    foreach (var element in (IEnumerable)state.CurrentInput)
                    {
                        state.CurrentInput = element;
                        var node = new XmlWriterNode<TInput>(state.Current);
                        var result = func(node)(state);
                        state.Current = oldNode;
                    }
                    state.CurrentInput = oldInput;
                    return new Result<TOut>(default(TOut), state);
                };
        }

        //public static XmlWriter<object> Many<TInput>(this XmlWriter<IEnumerable<TInput>> writer)
        //{
        //    return
        //        state =>
        //        {
        //            var oldInput = state.CurrentInput;
        //            var oldNode = state.Current;
        //            foreach (var element in (IEnumerable)state.CurrentInput)
        //            {
        //                state.CurrentInput = element;
        //                writer(state);
        //                state.Current = oldNode;
        //            }
        //            state.CurrentInput = oldInput;
        //            return new Result<object>(null, state);
        //        };
        //}

        //public static XmlWriter<object> Many<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, string> func)
        //{
        //    return
        //        state =>
        //        {
        //            var oldInput = state.CurrentInput;
        //            var oldNode = state.Current;
        //            foreach (var element in (IEnumerable)state.CurrentInput)
        //            {
        //                state.CurrentInput = element;
        //                writer(state);
        //                state.Current = oldNode;
        //            }
        //            state.CurrentInput = oldInput;
        //            return new Result<object>(null, state);
        //        };
        //}
    }
}
