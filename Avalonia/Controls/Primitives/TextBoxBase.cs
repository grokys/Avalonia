// -----------------------------------------------------------------------
// <copyright file="TextBoxBase.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System;
    using Avalonia.Media;

    [TemplatePartAttribute(Name = "PART_ContentHost", Type = typeof(FrameworkElement))]
    public class TextBoxBase : Control
    {
        public static readonly DependencyProperty CaretBrushProperty =
            DependencyProperty.Register(
                "CaretBrush",
                typeof(Brush),
                typeof(TextBoxBase),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush CaretBrush 
        {
            get { return (Brush)this.GetValue(CaretBrushProperty); }
            set { this.SetValue(CaretBrushProperty, value); }
        }
    }
}
