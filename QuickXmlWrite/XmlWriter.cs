﻿using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public delegate IResult<TOutput> XmlWriter<out TOutput>(State input);

    public class XmlWriterNode
    {
        private readonly Node node;

        public XmlWriterNode(Node node)
        {
            this.node = node;
        }
    }
}