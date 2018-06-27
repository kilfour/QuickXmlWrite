using System.Collections.Generic;
using System.Text;

namespace QuickXmlWrite.XmlStructure
{
	public class Node : Item
	{
		public string Name;
		public List<Item> Children;
		public Dictionary<string, string> Attributes;

	    public Node()
	    {
	        Children = new List<Item>();
	        Attributes = new Dictionary<string, string>();

        }

	    public override string AsString()
	    {
	        var builder = new StringBuilder();
	        builder.AppendFormat("<{0}", Name);
	        foreach (var attribute in Attributes)
	        {
                builder.AppendFormat(" {0}=\"{1}\"", attribute.Key, attribute.Value);
	        }
	        if (Children.Count == 0)
	        {
	            builder.Append(" />");
	            return builder.ToString();
            }
            builder.Append(">");
	        foreach (var child in Children)
	        {
	            builder.Append(child.AsString());
	        }
            builder.AppendFormat("</{0}>", Name);
	        return builder.ToString();
	    }
	}
}