using Xunit;

namespace QuickXmlWrite.Tests
{
    public class Chaining
    {
        [Fact]
        public void TagAndContent()
        {
            var writer =
                from root in XmlWrite<string>.Tag("root").Content("content")
                select root;
            var expected = "<root>content</root>";
            var actual = writer.Write("");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TwoTagsAndContent()
        {
            var writer =
                from root in XmlWrite<string>.Tag("root").Tag("child").Content("content")
                select root;

            var expected = "<root><child>content</child></root>";
            var actual = writer.Write("");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PassedOn()
        {
            var writer =
                from root in XmlWrite<string>.Tag("root")
                from odTitle in root.Tag("child").Tag("grandchild").Content("content")
                select root;
            
            var expected = "<root><child><grandchild>content</grandchild></child></root>";
            var actual = writer.Write("");
            Assert.Equal(expected, actual);
        }
    }
}