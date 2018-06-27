using System;
using System.Runtime.Remoting.Messaging;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite.UnderTheHood
{
    public class XmlWriterNode<TInput>
    {
        public  Node Node { get; set; }

        public XmlWriterNode(Node node)
        {
            this.Node = node;
        }

        public XmlWriter<XmlWriterNode<TInput>> Content(string text)
        {
            return state =>
            {
                var content = new Content { Text = text };
                Node.Add(content);
                return new Result<XmlWriterNode<TInput>>(this, state);
            };
        }

        public XmlWriter<XmlWriterNode<TInput>> Content(Func<TInput, string> func)
        {
            return state =>
            {
                var content = new Content { Text = func((TInput)state.CurrentInput) };
                Node.Add(content);
                return new Result<XmlWriterNode<TInput>>(this, state);
            };
        }

        public XmlWriter<XmlWriterNode<TInput>> Attribute(string name, string value)
        {
            return state =>
            {
                Node.Attributes.Add(name, value);
                return new Result<XmlWriterNode<TInput>>(this, state);
            };
        }

        public XmlWriter<XmlWriterNode<TInput>> Attribute(string name, Func<TInput, string> func)
        {
            return state =>
            {
                Node.Attributes.Add(name, func((TInput)state.CurrentInput));
                return new Result<XmlWriterNode<TInput>>(this, state);
            };
        }

        public XmlWriter<XmlWriterNode<TInput>> Tag(string tag)
        {
            return state =>
            {
                if (Node == null)
                {
                    state.AppendTag(tag);
                    return Result<TInput>.FromState(state);
                }
                var newnode = new Node { Name = tag };
                Node.Add(newnode);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(newnode), state);
            };
        }

        public XmlWriter<XmlWriterNode<TInput>> Tag(Func<TInput, string> func)
        {
            return state =>
            {
                if (Node == null)
                {
                    state.AppendTag(func((TInput)state.CurrentInput));
                    return Result<TInput>.FromState(state);
                }
                var newnode = new Node { Name = func((TInput)state.CurrentInput) };
                Node.Add(newnode);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(newnode), state);
            };
        }
    }
}