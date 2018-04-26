namespace QuickXmlWrite
{
    public interface IResult<out TValue>
    {
        TValue Value { get; }
        string AsString();
    }
}