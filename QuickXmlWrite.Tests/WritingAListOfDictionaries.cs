using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingAListOfDictionaries
    {
        private readonly List<Dictionary<string, string>> input = new List<Dictionary<string, string>>(
            new[]
            {
                new Dictionary<string, string> { {"k1", "v1"}, {"k2", "v2"} },
                new Dictionary<string, string> { {"k1", "v3"}, {"k2", "v4"} }
            }
        );

        private string expected = "<root><dict><k1>v1</k1><k2>v2</k2></dict><dict><k1>v3</k1><k2>v4</k2></dict></root>";

        [Fact]
        public void Composed()
        {
            var keyValueWriter = XmlWrite.For<KeyValuePair<string, string>>().Tag(x => x.Key).Content(x => x.Value);
            var dictionaryWriter =
                from root in XmlWrite.For<Dictionary<string, string>>().Tag("dict")
                from sub in keyValueWriter.Many()
                select root;
            var writer =
                from root in XmlWrite.For<List<Dictionary<string, string>>>().Tag("root")
                from sub in dictionaryWriter.Many()
                select root;
            Assert.Equal(expected, writer.Write(input));
        }

        [Fact]
        public void Inline()
        {
            var writer =
                from root in XmlWrite.For<List<Dictionary<string, string>>>().Tag("root")
                let keyValueWriter = XmlWrite.For<KeyValuePair<string, string>>().Tag(x => x.Key).Content(x => x.Value)
                let dictionaryWriter =
                    from someThing in XmlWrite.For<Dictionary<string, string>>().Tag("dict")
                    from sub in keyValueWriter.Many()
                    select root
                from sub in dictionaryWriter.Many()
                select root;
            Assert.Equal(expected, writer.Write(input));
        }

        

        [Fact]
        public void BetterInlineTyped()
        {
            var writer =
                XmlWrite.For<List<Dictionary<string, string>>>().Tag("root")
                    .Many(list => list, dict => dict.Tag("dict")
                        .Many(x => x.ToList(), kv => kv.Tag(x => x.Key).Content(x => x.Value)));

            Assert.Equal(expected, writer.Write(input));
        }

        [Fact]
        public void BetterInlineTypedComposed()
        {
            var writer =
                from root in XmlWrite.For<List<Dictionary<string, string>>>().Tag("root")
                from subs in root.Many(list => list, dict => dict.Tag("dict")
                        .Many(x => x.ToList(), kv => kv.Tag(x => x.Key).Content(x => x.Value)))
                select root;

            Assert.Equal(expected, writer.Write(input));
        }
    }
}