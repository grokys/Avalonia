// -----------------------------------------------------------------------
// <copyright file="TextBlock.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System.Globalization;
    using System.Windows.Markup;
    using Avalonia.Media;

    [ContentProperty("Text")]
    public class TextBlock : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush),
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.Transparent),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure,
                    TextChanged));

        private FormattedText formattedText;

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public string Text 
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(new Point(), new Size(this.ActualWidth, this.ActualHeight));

            if (this.formattedText == null)
            {
                this.formattedText = this.CreateFormattedText();
            }

            drawingContext.DrawRectangle(
                this.Background,
                null,
                rect);

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

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextBlock)d).formattedText = null;
        }

        private FormattedText CreateFormattedText()
        {
            return new FormattedText(
                this.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(),
                12,
                new SolidColorBrush(Colors.Black));
        }
    }
}
