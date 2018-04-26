using System.Collections;
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
                    return new Result<Content>(null, state);
                };
        }
    }
}
