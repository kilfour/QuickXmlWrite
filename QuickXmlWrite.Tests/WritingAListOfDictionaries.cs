using System.Collections.Generic;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingAListOfDictionaries
    {
        [Fact]
        public void Composed()
        {
            var keyValueWriter = XmlWrite<KeyValuePair<string, string>>.Tag(x => x.Key).Content(x => x.Value);
            var dictionaryWriter =
                from root in XmlWrite<IDictionary<string, string>>.Tag("SomeThing")
                from sub in keyValueWriter.Many()
                select root;
            var writer =
                from root in XmlWrite<List<IDictionary<string, string>>>.Tag("root")
                from sub in dictionaryWriter.Many()
                select root;
            var expected = "<root><SomeThing><keyone>1</keyone><keytwo>2</keytwo></SomeThing><SomeThing><keyone>3</keyone><keytwo>4</keytwo></SomeThing></root>";
            var input = new List<Dictionary<string, string>>(
                new[]
                {
                    new Dictionary<string, string>
                    {
                        {"keyone", "1"},
                        {"keytwo", "2"}
                    },
                    new Dictionary<string, string>
                    {
                        {"keyone", "3"},
                        {"keytwo", "4"}
                    }
                }
            );
            var actual = writer.Write(input);
            Assert.Equal(expected, actual);
        }
    }
}