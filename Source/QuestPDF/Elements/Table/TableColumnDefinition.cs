namespace QuestPDF.Elements.Table
{
    internal class TableColumnDefinition
    {
        public bool Auto { get; }
        public float ConstantSize { get; internal set; }
        public float RelativeSize { get; }

        internal float Width { get; set; }

        public TableColumnDefinition(float constantSize, float relativeSize)
        {
            ConstantSize = constantSize;
            RelativeSize = relativeSize;
        }

        public TableColumnDefinition()
        {
            Auto = true;
        }
    }
}