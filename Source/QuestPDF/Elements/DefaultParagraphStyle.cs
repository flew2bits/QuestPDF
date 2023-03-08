using QuestPDF.Infrastructure;

namespace QuestPDF.Elements
{
    internal class DefaultParagraphStyle : ContainerElement
    {
        public ParagraphStyle ParagraphStyle { get; set; } = ParagraphStyle.Default;
    }
}