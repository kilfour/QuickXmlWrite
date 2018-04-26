using System;
using System.Linq.Expressions;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite
{
    public static class XmlWrite<TInput>
    {
        public static XmlWriter<Node, TInput> Tag(string tag)
        {
            return 
                state =>
                {
                    state.AppendTag(tag);
                    return new Result<Node>(state.Current as Node, state);
                };
        }

        public static XmlWriter<Node, TInput> Tag(Func<object, string> func)
        {
            return
                state =>
                {
                    state.AppendTag(func(state.CurrentInput));
                    return new Result<Node>(state.Current as Node, state);
                };
        }
    }
}
