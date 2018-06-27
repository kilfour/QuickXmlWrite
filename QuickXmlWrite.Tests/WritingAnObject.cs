using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingAnObject
    {
        [Fact]
        public void Verbose()
        {
            var writer =
                from input in XmlWrite.For<MyThing>()
                from root in input.Tag("root")
                from myString in root.Tag("string")
                from myStringContent in myString.Content(x => x.MyString)
                from myInt in root.Tag("int")
                from myIntContent in myInt.Content(x => x.MyInt.ToString())
                select root;
            var expected = "<root><string>some text</string><int>42</int></root>";

            var thing = new MyThing {MyString = "some text", MyInt = 42};
            var actual = writer.Write(thing);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LessVerbose()
        {
            var writer =
                from root in XmlWrite.For<MyThing>().Tag("root")
                from myStringContent in root.Tag("string").Content(x => x.MyString)
                from myIntContent in root.Tag("int").Content(x => x.MyInt.ToString())
                select root;
            var expected = "<root><string>some text</string><int>42</int></root>";

            var thing = new MyThing { MyString = "some text", MyInt = 42 };
            var actual = writer.Write(thing);
            Assert.Equal(expected, actual);
        }

        public class MyThing
        {
            public string MyString { get; set; }
            public int MyInt { get; set; }
        }
    }
}