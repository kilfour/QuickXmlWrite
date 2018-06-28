namespace QuickXmlWrite.UnderTheHood
{
    public class Result<TValue> : IResult<TValue>
    {
        public TValue Value { get; }
        public readonly State State;
        public Result(TValue value, State state) { Value = value; State = state; }

        public string AsString()
        {
            return State.AsString();
        }

        public static Result<XmlWriterNode<TValue>> WriterNodeResultFromState(State state)
        {
            return new Result<XmlWriterNode<TValue>>(new XmlWriterNode<TValue>(state.Current), state);
        }
    }
}