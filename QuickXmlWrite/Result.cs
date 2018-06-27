using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public class Result<TValue> : IResult<TValue>
    {
        public TValue Value { get; private set; }
        public readonly State State;
        public Result(TValue value, State state) { Value = value; State = state; }

        public string AsString()
        {
            return State.AsString();
        }

        public static Result<XmlWriterNode<TValue>> FromState(State state)
        {
            return new Result<XmlWriterNode<TValue>>(new XmlWriterNode<TValue>(state.Current), state);
        }
    }
}