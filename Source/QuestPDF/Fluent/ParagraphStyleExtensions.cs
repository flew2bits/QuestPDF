using QuestPDF.Infrastructure;

namespace QuestPDF.Fluent
{
    public static class ParagraphStyleExtensions
    {
        public static ParagraphStyle LeftIndent(this ParagraphStyle style, float value)
        {
            return style.Mutate(ParagraphStyleProperty.LeftIndent, value);
        }

        public static ParagraphStyle RightIndent(this ParagraphStyle style, float value)
        {
            return style.Mutate(ParagraphStyleProperty.RightIndent, value);
        }

        public static ParagraphStyle FirstLineLeftIndent(this ParagraphStyle style, float value)
        {
            return style.Mutate(ParagraphStyleProperty.FirstLineLeftIndent, value);
        }

        public static ParagraphStyle Spacing(this ParagraphStyle style, float value)
        {
            return style.Mutate(ParagraphStyleProperty.Spacing, value);
        }

        public static ParagraphStyle TextAlignment(this ParagraphStyle style, TextAlignment value)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, value);
        }

        public static ParagraphStyle DropCapLines(this ParagraphStyle style, TextAlignment value)
        {
            return style.Mutate(ParagraphStyleProperty.DropCapLines, value);
        }
        
        #region Alignment

        public static ParagraphStyle AlignLeft(this ParagraphStyle style)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, Infrastructure.TextAlignment.Left);
        }

        public static ParagraphStyle AlignCenter(this ParagraphStyle style)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, Infrastructure.TextAlignment.Center);
        }

        public static ParagraphStyle AlignRight(this ParagraphStyle style)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, Infrastructure.TextAlignment.Right);
        }

        public static ParagraphStyle AlignJustifyLastLeft(this ParagraphStyle style)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, Infrastructure.TextAlignment.JustifyLastLeft);
        }

        public static ParagraphStyle AlignJustifyLastCenter(this ParagraphStyle style)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, Infrastructure.TextAlignment.JustifyLastCenter);
        }

        public static ParagraphStyle AlignJustifyLastRight(this ParagraphStyle style)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, Infrastructure.TextAlignment.JustifyLastRight);
        }
        
        public static ParagraphStyle AlignJustifyAll(this ParagraphStyle style)
        {
            return style.Mutate(ParagraphStyleProperty.TextAlignment, Infrastructure.TextAlignment.JustifyAll);
        }
        
        #endregion
    }
}