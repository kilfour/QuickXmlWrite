using System;
using QuickXmlWrite.UnderTheHood;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {

        public static XmlWriter<XmlWriterNode<TInput>> If<TInput>(this XmlWriterNode<TInput> writerNode, Func<TInput, bool> predicate)
        {
            return state => 
            {
                var condition = predicate((TInput)state.CurrentInput);
                if(condition)
                    return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(state.Current), state);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(new Node()), state); 
            };
        }

        public static XmlWriter<XmlWriterNode<TInput>> If<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer, Func<TInput, bool> predicate)
        {
            return state =>
            {
                var condition = predicate((TInput)state.CurrentInput);
                if (condition)
                    return writer(state);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(new Node()), state);
            };
        }
    }
}
