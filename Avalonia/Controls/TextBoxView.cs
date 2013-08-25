// -----------------------------------------------------------------------
// <copyright file="TextBoxView.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System.Globalization;
    using Avalonia.Media;

    internal class TextBoxView : FrameworkElement
    {
        private FormattedText formattedText;

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(new Point(), new Size(this.ActualWidth, this.ActualHeight));

            if (this.formattedText == null)
            {
                this.formattedText = this.CreateFormattedText();
            }

            drawingContext.DrawText(this.formattedText, new Point());
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.formattedText == null)
            {
                this.formattedText = this.CreateFormattedText();
            }

            return new Size(this.formattedText.Width, this.formattedText.Height);
        }

        private FormattedText CreateFormattedText()
        {
            TextBox textBox = VisualTreeHelper.GetAncestor<TextBox>(this);

            return new FormattedText(
                textBox.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBox.FontFamily, textBox.FontStyle, textBox.FontWeight, textBox.FontStretch),
                textBox.FontSize,
                textBox.Foreground);
        }
    }
}
