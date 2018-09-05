

using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public static partial class XmlWriteExtensions
    {
        public static XmlWriter<XmlWriterNode<TInput>> Apply<TInput>(this XmlWriterNode<TInput> writerNode, XmlWriter<XmlWriterNode<TInput>> innerWriter)
        {
            return state =>
            {
                var oldNode = state.Current;
                innerWriter(state);
                state.Current = oldNode;
                return Result<TInput>.WriterNodeResultFromState(state);
            };
        }

        public static XmlWriter<XmlWriterNode<TInput>> Apply<TInput>(this XmlWriterNode<TInput> writerNode, XmlWriterAction<TInput> innerWriterAction)
        {
            return state =>
            {
                var oldInput = state.CurrentInput;
                var oldNode = state.Current;
                IResult<XmlWriterNode<TInput>> InnerWriter(State innerState) => new Result<XmlWriterNode<TInput>>(new XmlWriterNode<TInput>(innerState.Current), innerState);
                var appliedWriter = innerWriterAction(InnerWriter);
                appliedWriter(state);
                state.Current = oldNode;
                state.CurrentInput = oldInput;
                return Result<TInput>.WriterNodeResultFromState(state);
            };
        }
    }
}