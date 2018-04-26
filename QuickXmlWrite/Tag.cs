using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static class XmlWrite<TInput>
    {
        public static XmlWriter<XmlWriterNode<TInput>> Tag(string tag)
        {
            return 
                state =>
                {
                    state.AppendTag(tag);
                    return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(state.Current as Node), state);
                };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Tag(Func<TInput, string> func)
        {
            return
                state =>
                {
                    state.AppendTag(func((TInput)state.CurrentInput));
                    return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(state.Current as Node), state);
                };
        }
    }
}
