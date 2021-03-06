﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickXmlWrite.XmlStructure
{
	public class Node : Item
	{
		public string Name;
		private List<Item> Children;
		public Dictionary<string, string> Attributes;

	    public Node()
	    {
	        Children = new List<Item>();
	        Attributes = new Dictionary<string, string>();

        }

	    public void Add(Item item)
	    {
	        item.Parent = this;
            Children.Add(item);
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
	            builder.Append("/>");
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

	    public override string AsHumanReadableString(int level, int numberOfSpacesPerLevel, bool htmlEncoded)
	    {
	        var builder = new StringBuilder();
            builder.AppendLine();
            builder.Append(new string(' ', level * numberOfSpacesPerLevel));
	        builder.AppendFormat("<{0}", Name);
	        foreach (var attribute in Attributes)
	        {
	            builder.AppendFormat(" {0}=\"{1}\"", attribute.Key, attribute.Value);
	        }
	        if (Children.Count == 0)
	        {
	            builder.Append(" />");
	            builder.AppendLine();
                return builder.ToString();
	        }
	        builder.Append(">");
            foreach (var child in Children)
	        {
	            builder.Append(child.AsHumanReadableString(level + 1, numberOfSpacesPerLevel, htmlEncoded));
	        }
	        var lastChild = Children.Last();
	        if (lastChild is Node)
	        {
	            builder.AppendLine();
	            builder.Append(new string(' ', level * numberOfSpacesPerLevel));
            }
            builder.AppendFormat("</{0}>", Name);
            return builder.ToString();
        }
	}
}