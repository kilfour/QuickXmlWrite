namespace QuickXmlWrite.UnderTheHood
{
    public interface IResult<out TValue>
    {
        TValue Value { get; }
        string AsString();
        string AsHumanReadableString();
    }
}