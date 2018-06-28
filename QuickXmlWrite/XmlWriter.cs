using QuickXmlWrite.UnderTheHood;

namespace QuickXmlWrite
{
    public delegate IResult<TOutput> XmlWriter<out TOutput>(State input);
}