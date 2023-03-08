using System;
using System.Collections.Concurrent;

namespace QuestPDF.Infrastructure
{
    internal enum ParagraphStyleProperty
    {
        LeftIndent,
        RightIndent,
        FirstLineLeftIndent,
        Spacing,
        TextAlignment,
        DropCapLines
    }
    
    internal static class ParagraphStyleManager
    {
        private static readonly
            ConcurrentDictionary<(ParagraphStyle origin, ParagraphStyleProperty property, object value), ParagraphStyle>
            ParagraphStyleMutateCache = new();

        private static readonly ConcurrentDictionary<(ParagraphStyle origin, ParagraphStyle parent), ParagraphStyle>
            ParagraphStyleApplyGlobalCache = new();

        private static readonly ConcurrentDictionary<(ParagraphStyle origin, ParagraphStyle parent), ParagraphStyle>
            ParagraphStyleOverrideCache = new();

        public static ParagraphStyle Mutate(this ParagraphStyle origin, ParagraphStyleProperty property, object value)
        {
            var cacheKey = (origin, property, value);
            return ParagraphStyleMutateCache.GetOrAdd(cacheKey, x => MutateStyle(x.origin, x.property, x.value));
        }
        
        private static ParagraphStyle MutateStyle(this ParagraphStyle origin, ParagraphStyleProperty property, object? value,
            bool overrideValue = true)
        {
            if (overrideValue && value == null)
                return origin;
            
            switch (property)
            {
                case ParagraphStyleProperty.LeftIndent:
                {
                    if (!overrideValue && origin.LeftIndent != null) return origin;
                    var castedValue = (float?)value;
                    if (origin.LeftIndent == castedValue)
                        return origin;

                    return origin with { LeftIndent = castedValue };
                }
                case ParagraphStyleProperty.RightIndent:
                {
                    if (!overrideValue && origin.RightIndent != null) return origin;
                    var castedValue = (float?)value;
                    if (origin.RightIndent == castedValue)
                        return origin;

                    return origin with { RightIndent = castedValue };
                }
                case ParagraphStyleProperty.FirstLineLeftIndent:
                {
                    if (!overrideValue && origin.FirstLineLeftIndent != null) return origin;
                    var castedValue = (float?)value;
                    if (origin.FirstLineLeftIndent == castedValue)
                        return origin;

                    return origin with { FirstLineLeftIndent = castedValue };
                }
                case ParagraphStyleProperty.Spacing:
                {
                    if (!overrideValue && origin.Spacing != null) return origin;
                    var castedValue = (float?)value;
                    if (castedValue < 0) return origin;
                    if (origin.Spacing == castedValue)
                        return origin;

                    return origin with { Spacing = castedValue };
                }
                case ParagraphStyleProperty.DropCapLines:
                {
                    if (!overrideValue && origin.DropCapLines != null) return origin;
                    var castedValue = (int?)value;
                    if (castedValue!.Value < 1) return origin;
                    if (origin.DropCapLines == castedValue)
                        return origin;

                    return origin with { DropCapLines = castedValue };
                }
                case ParagraphStyleProperty.TextAlignment:
                {
                    if (!overrideValue && origin.TextAlignment != null)
                        return origin;
                
                    var castedValue = (TextAlignment?)value;
                
                    if (origin.TextAlignment == castedValue)
                        return origin;

                    return origin with { TextAlignment = castedValue };
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(property), property, "Expected to mutate the ParagraphStyle object. Provided property type is not supported.");
            }
        }

        internal static ParagraphStyle ApplyGlobalStyle(this ParagraphStyle style, ParagraphStyle parent)
        {
            var cacheKey = (style, parent);
            return ParagraphStyleApplyGlobalCache.GetOrAdd(cacheKey,
                key => ApplyStyle(key.origin, key.parent, overrideStyle: false));
        }

        internal static ParagraphStyle OverrideStyle(this ParagraphStyle style, ParagraphStyle parent)
        {
            var cacheKey = (style, parent);

            return ParagraphStyleOverrideCache.GetOrAdd(cacheKey, key =>
            {
                var result = ApplyStyle(key.origin, key.parent);
                return result;
            });
        }
        
        internal static ParagraphStyle ApplyStyle(this ParagraphStyle style, ParagraphStyle parent,
            bool overrideStyle = true)
        {
            var result = style;

            result = MutateStyle(result, ParagraphStyleProperty.LeftIndent, parent.LeftIndent, overrideStyle);
            result = MutateStyle(result, ParagraphStyleProperty.RightIndent, parent.RightIndent, overrideStyle);
            result = MutateStyle(result, ParagraphStyleProperty.FirstLineLeftIndent, parent.FirstLineLeftIndent, overrideStyle);
            result = MutateStyle(result, ParagraphStyleProperty.Spacing, parent.Spacing, overrideStyle);
            result = MutateStyle(result, ParagraphStyleProperty.TextAlignment, parent.TextAlignment, overrideStyle);
            result = MutateStyle(result, ParagraphStyleProperty.DropCapLines, parent.DropCapLines, overrideStyle);

            return result;
        }
    }
}