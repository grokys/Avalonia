// -----------------------------------------------------------------------
// <copyright file="TextBlock.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using Avalonia.Media;

    public class TextBlock : FrameworkElement
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        public TextBlock()
        {
            this.FontSize = 12;
        }

        public double FontSize { get; set; }

        public string Text { get; set; }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawText(
                new FormattedText { Text = this.Text },
                new Point());
        }
    }
}
