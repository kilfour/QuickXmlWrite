using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Up<TInput>(this XmlWriter<XmlWriterNode<TInput>> writer)
        {
            return
                state =>
                {
                    var result = writer(state);
                    state.Current = result.Value.Node.Parent;
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }
    }
}
