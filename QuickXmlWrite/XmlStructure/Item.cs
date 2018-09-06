namespace QuickXmlWrite.XmlStructure
{
    public abstract class Item
    {
        public abstract string AsString();
        public abstract string AsHumanReadableString(int level, int numberOfSpacesPerLevel);
        public Node Parent;
    }
}