using System;
using QuickXmlWrite.XmlStructure;

namespace QuickXmlWrite.UnderTheHood
{
    public class State
    {
        public readonly Document Document = new Document();
        public Node Current { get; set; }
        public object CurrentInput { get; set; }
        
        public string GetValue<T>(Func<T, string> func)
        {
            return func((T)CurrentInput);
        }

        public string AsString()
        {
            return Document.Root.AsString();
        }

        public string AsHumanReadableString()
        {
            return Document.Root.AsHumanReadableString(0);
        }
    }
}