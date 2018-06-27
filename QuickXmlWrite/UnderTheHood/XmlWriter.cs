namespace QuickXmlWrite.UnderTheHood
{
    public delegate IResult<TOutput> XmlWriter<out TOutput>(State input);
}