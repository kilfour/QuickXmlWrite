using Xunit;

namespace QuickXmlWrite.Tests
{
    public class Attributes
    {   
        [Fact]
        public void HardCoded()
        {
            var writer =
                from input in XmlWrite.For<string>()
                from root in input.Tag("root")
                from child in root.Tag("child")
                from content in child.Attribute("theName", "TheValue")
                select root;
            var expected = "<root><child theName=\"TheValue\"/></root>";
            var actual = writer.Write("");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dynamic()
        {
            var writer =
                from root in XmlWrite.For<string>().Tag("root")
                from child in root.Tag("child")
                from content in child.Attribute("theName", x => x)
                select root;
            var expected = "<root><child theName=\"yep\"/></root>";
            var actual = writer.Write("yep");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChainedDynamic()
        {
            var writer = XmlWrite.For<string>().Tag("root").Attribute("theName", x => x);
            var expected = "<root theName=\"yep\"/>";
            var actual = writer.Write("yep");
            Assert.Equal(expected, actual);
        }

    }
}
