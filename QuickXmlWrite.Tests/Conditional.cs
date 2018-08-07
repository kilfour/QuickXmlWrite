using QuickXmlWrite.UnderTheHood;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class Conditional
    {
        private static readonly XmlWriter<XmlWriterNode<int>> Writer = 
            from root in XmlWrite.For<int>().Tag("root")
            from sub in root.Tag("int").Content(x => x.ToString()).If(x => x == 42)
            select root;

        [Fact]
        public void IsTrue()
        {
            Assert.Equal("<root><int>42</int></root>", Writer.Write(42));
        }

        [Fact]
        public void IsFalse()
        {
            Assert.Equal("<root/>", Writer.Write(666));
        }
    }
}