using System.Net;

namespace QuickXmlWrite.XmlStructure
{
	public class Content : Item
	{
	    public string Text;
	    public bool AsCData;
	    public override string AsString()
	    {
	        return Text;
	    }

	    public override string AsHumanReadableString(int level, int numberOfSpacesPerLevel, bool htmlEncoded)
	    {
	        if (AsCData)
	        {
	            return $"<![CDATA[{GetText(htmlEncoded)}]]>";
	        }
	        return GetText(htmlEncoded);
	    }

	    private string GetText(bool htmlEncoded)
	    {
	        if (htmlEncoded)
	            return WebUtility.HtmlEncode(Text);
	        return Text;
        }
    }
}