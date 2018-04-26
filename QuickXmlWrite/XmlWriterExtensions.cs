﻿using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static class XmlWriterExtensions
    {
        public static string Write<TInput>(this XmlWriter<Node, TInput> writer, TInput input)
        {
            var state = new State();
            state.CurrentInput = input;
            var result = writer(state);
            return result.AsString();
        }
    }
}