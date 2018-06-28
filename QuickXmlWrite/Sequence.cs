using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Sequence<TInput>(
            this XmlWriter<XmlWriterNode<TInput>> writer,
            params XmlWriterAction<TInput>[] writerActions)
        {
            return
                state =>
                {
                    
                    var result = writer(state);
                    state.Current = result.Value.Node;
                    var oldNode = state.Current;
                    IResult<XmlWriterNode<TInput>> InnerWriter(State innerState) => Result<TInput>.WriterNodeResultFromState(innerState);
                    foreach (var innerWriterAction in writerActions)
                    {
                        var appliedWriter = innerWriterAction(InnerWriter);
                        appliedWriter(state);
                        state.Current = oldNode;
                    }
                    return Result<TInput>.WriterNodeResultFromState(state);
                };
        }

        
    }
}
