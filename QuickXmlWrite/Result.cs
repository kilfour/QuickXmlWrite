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
    }
}