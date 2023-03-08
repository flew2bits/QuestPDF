using System;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using QuestPDF.Elements.Text;
using QuestPDF.Elements.Text.Items;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDF.Fluent
{
    public static class DebugExtensions
    {
        public static IContainer DebugArea(this IContainer parent, string text, string color)
        {
            var container = new Container();

            parent.Component(new DebugArea
            {
                Child = container,
                Text = text,
                Color = color
            });

            return container;
        }
        
        public static IContainer DebugArea(this IContainer parent, string text)
        {
            return parent.DebugArea(text, Colors.Red.Medium);
        }

        public static IContainer DebugArea(this IContainer parent)
        {
            return parent.DebugArea(string.Empty, Colors.Red.Medium);
        }
        
        /// <summary>
        /// Creates a virtual element that is visible on the elements trace when the layout overflow exception is thrown.
        /// This can be used to easily identify elements inside the elements trace tree and faster find issue root cause.
        /// </summary>
        public static IContainer DebugPointer(this IContainer parent, string elementTraceText)
        {
            return parent.DebugPointer(elementTraceText, true);
        }
        
        internal static IContainer DebugPointer(this IContainer parent, string elementTraceText, bool highlight)
        {
            return parent.Element(new DebugPointer
            {
                Target = elementTraceText,
                Highlight = highlight
            });
        }

        public static void GenerateDebug(this Document document)
        {
            var container = new DocumentContainer();
            document.Compose(container);
            var content = container.Compose();
            
            content.ApplyDefaultTextStyle(TextStyle.LibraryDefault);
            content.ApplyDefaultParagraphStyle(ParagraphStyle.LibraryDefault);
            content.ApplyContentDirection(ContentDirection.LeftToRight);
            
            DebugElement(content);
        }

        private static void DebugElement(Element? element, int level = 0)
        {
            if (element is null) return;

            var levelIndent = new string(' ', level);
            
            switch (element)
            {
                case TextBlock textBlock:
                {
                    Console.WriteLine(levelIndent + "TextBlock");
                    foreach (var textBlockItem in textBlock.Items)
                        switch (textBlockItem)
                        {
                            case TextBlockSpan span: 
                                Console.WriteLine(levelIndent + " " + $"TextSpan: TS:{span.Style.GetHashCode():X8} PS:{span.ParagraphStyle.GetHashCode():X8} {span.Text}");
                                Console.WriteLine(levelIndent + "  - LI: " + span.ParagraphStyle.LeftIndent);
                                Console.WriteLine(levelIndent + "  - FLLI: " + span.ParagraphStyle.FirstLineLeftIndent);
                                Console.WriteLine(levelIndent + "  - RI: " + span.ParagraphStyle.RightIndent);
                                Console.WriteLine(levelIndent + "  - SP: " + span.ParagraphStyle.Spacing);
                                Console.WriteLine(levelIndent + "  - TA: " + span.ParagraphStyle.TextAlignment);
                                Console.WriteLine(levelIndent + "  - DC: " + span.ParagraphStyle.DropCapLines);
                                break;
                            case TextBlockElement textElement:
                                DebugElement(textElement.Element, level + 1);
                                break;
                        }

                    return;
                }
                case Column column:
                    Console.WriteLine(levelIndent + $"Column: SP - {column.Spacing}");
                    break;
                case DebugPointer debugPointer:
                    Console.WriteLine(levelIndent + $"* {debugPointer.Target} *");
                    break;
                case Extend extend:
                    Console.WriteLine(levelIndent + $"Extend: H - {extend.ExtendHorizontal}, V - {extend.ExtendVertical}");
                    break;
                case DefaultTextStyle textStyle:
                    Console.WriteLine(levelIndent + $"TextStyle: {textStyle.TextStyle.GetHashCode():X8}");
                    break;
                case DefaultParagraphStyle paragraphStyle:
                    Console.WriteLine(levelIndent + "ParagraphStyle");
                    Console.WriteLine(levelIndent + " - LI: " + paragraphStyle.ParagraphStyle.LeftIndent);
                    Console.WriteLine(levelIndent + " - FLLI: " + paragraphStyle.ParagraphStyle.FirstLineLeftIndent);
                    Console.WriteLine(levelIndent + " - RI: " + paragraphStyle.ParagraphStyle.RightIndent);
                    Console.WriteLine(levelIndent + " - SP: " + paragraphStyle.ParagraphStyle.Spacing);
                    Console.WriteLine(levelIndent + " - TA: " + paragraphStyle.ParagraphStyle.TextAlignment);
                    Console.WriteLine(levelIndent + " - DC: " + paragraphStyle.ParagraphStyle.DropCapLines);
                    break;               
                default:
                    Console.WriteLine(levelIndent + element.GetType().Name);
                    break;
            }
            foreach (var child in element.GetChildren()) DebugElement(child, level + 1);
        }
    }
}