using System;
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

        public XmlWriter<Content> Content(string text)
        {
            return state =>
            {
                var content = new Content { Text = text };
                Node.Children.Add(content);
                return new Result<Content>(content, state);
            };
        }

        public XmlWriter<Content> Content(Func<TInput, string> func)
        {
            return state =>
            {
                var content = new Content { Text = func((TInput)state.CurrentInput) };
                Node.Children.Add(content);
                return new Result<Content>(content, state);
            };
        }

        public XmlWriter<XmlWriterNode<TInput>> Tag(string tag)
        {
            return state =>
            {
                var newnode = new Node { Name = tag };
                Node.Children.Add(newnode);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(newnode), state);
            };
        }

        public XmlWriter<XmlWriterNode<TInput>> Tag(Func<TInput, string> func)
        {
            return state =>
            {
                var newnode = new Node { Name = func((TInput)state.CurrentInput) };
                Node.Children.Add(newnode);
                return new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(newnode), state);
            };
        }
    }
}