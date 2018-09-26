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

        [Fact]
        public void MultipleAttributesFirstOnly()
        {
            var writer = from root in XmlWrite.For<int>().Tag("root")
                from sub1 in root.Attribute("attr1", "yep").If(x => x == 42)
                from sub2 in root.Attribute("attr2", "again").If(x => x != 42)
                select root;

            Assert.Equal("<root attr1=\"yep\"/>", writer.Write(42));
        }

        [Fact]
        public void MultipleAttributesSecondOnly()
        {
            var writer = from root in XmlWrite.For<int>().Tag("root")
                from sub1 in root.Attribute("attr1", "yep").If(x => x != 42)
                from sub2 in root.Attribute("attr2", "again").If(x => x == 42)
                select root;

            Assert.Equal("<root attr2=\"again\"/>", writer.Write(42));
        }

        [Fact]
        public void MultipleAttributesBoth()
        {
            var writer = from root in XmlWrite.For<int>().Tag("root")
                from sub1 in root.Attribute("attr1", "yep").If(x => x == 42)
                from sub2 in root.Attribute("attr2", "again").If(x => x == 42)
                select root;

            Assert.Equal("<root attr1=\"yep\" attr2=\"again\"/>", writer.Write(42));
        }
    }
}