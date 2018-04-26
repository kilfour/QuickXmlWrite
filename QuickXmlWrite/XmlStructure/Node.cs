using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
	        builder.Append(">");
	        foreach (var child in Children)
	        {
	            builder.Append(child.AsString());
	        }
            builder.AppendFormat("</{0}>", Name);
	        return builder.ToString();
	    }

	    public XmlWriter<Content, TInput> Content<TInput>(string text)
	    {
	        return state =>
	        {
	            var content = new Content{Text = text};
                Children.Add(content);
	            return new Result<Content>(content, state);
	        };
        }

	    public XmlWriter<Content, TInput> Content<TInput>(Func<TInput, string> func)
	    {
	        return state =>
	        {
	            var content = new Content { Text = func((TInput)state.CurrentInput) };
	            Children.Add(content);
	            return new Result<Content>(content, state);
	        };
	    }

	    public XmlWriter<Node, TInput> Tag<TInput>(string tag)
	    {
	        return state =>
	        {
	            var node = new Node { Name = tag };
	            Children.Add(node);
	            return new Result<Node>(node, state);
	        };
        }
	}
}