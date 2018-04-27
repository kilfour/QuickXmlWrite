using System.Collections.Generic;
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
            var keyValueWriter = XmlWrite<KeyValuePair<string, string>>.Tag(x => x.Key).Content(x => x.Value);
            var dictionaryWriter =
                from root in XmlWrite<IDictionary<string, string>>.Tag("dict")
                from sub in keyValueWriter.Many()
                select root;
            var writer =
                from root in XmlWrite<List<IDictionary<string, string>>>.Tag("root")
                from sub in dictionaryWriter.Many()
                select root;
            Assert.Equal(expected, writer.Write(input));
        }

        [Fact]
        public void Inline()
        {
            var writer =
                from root in XmlWrite<List<IDictionary<string, string>>>.Tag("root")
                let keyValueWriter = XmlWrite<KeyValuePair<string, string>>.Tag(x => x.Key).Content(x => x.Value)
                let dictionaryWriter =
                    from someThing in XmlWrite<IDictionary<string, string>>.Tag("dict")
                    from sub in keyValueWriter.Many()
                    select root
                from sub in dictionaryWriter.Many()
                select root;
            Assert.Equal(expected, writer.Write(input));
        }

        [Fact (Skip="WIP")]
        public void BetterInline()
        {
            var writer =
                from root in XmlWrite<IEnumerable<IEnumerable<KeyValuePair<string, string>>>>.Tag("root")
                from dict in root.ApplyMany(x => x.Tag("dict"))
                from kv in dict.ApplyMany(x => x.Tag(y => y.Key).Content(y => y.Value))
                select root;
            Assert.Equal(expected, writer.Write(input));
        }
    }
}