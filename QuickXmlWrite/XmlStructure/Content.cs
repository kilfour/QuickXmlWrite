namespace QuickXmlWrite.XmlStructure
{
	public class Content : Item
	{
		public string Text;
	    public override string AsString()
	    {
	        return Text;
	    }

	    public override string AsHumanReadableString(int level, int numberOfSpacesPerLevel)
	    {
            return Text;
	    }
    }
}