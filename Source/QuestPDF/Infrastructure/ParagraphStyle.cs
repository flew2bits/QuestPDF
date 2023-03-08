namespace QuestPDF.Infrastructure
{
    public record ParagraphStyle
    {
        internal float? LeftIndent { get; set; }
        
        internal float? RightIndent { get; set; }
        
        internal float? FirstLineLeftIndent { get; set; }
        
        internal float? Spacing { get; set; }
        
        internal TextAlignment? TextAlignment { get; set; }
        
        internal int? DropCapLines { get; set; }
        
        internal static ParagraphStyle LibraryDefault { get; } = new()
        {
            LeftIndent = 0,
            RightIndent =0,
            FirstLineLeftIndent = 0,
            Spacing = 0,
            TextAlignment = Infrastructure.TextAlignment.Auto,
            DropCapLines = 1
        };

        public static ParagraphStyle Default { get;  }= new();
    }
}