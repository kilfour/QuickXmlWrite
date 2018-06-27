using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExt
    {
        public static XmlWriter<XmlWriterNode<TInput>> Attribute<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, string name, string value)
        {
            return 
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node;
                    state.AppendAttribute(name, value);
                    return Result<TInput>.FromState(state);
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
                    return Result<TInput>.FromState(state);
                };

        }
    }
}
