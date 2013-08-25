// -----------------------------------------------------------------------
// <copyright file="TextBlock.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System.Globalization;
    using System.Windows.Markup;
    using Avalonia.Document;
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

        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    new FontFamily("Segoe UI"),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                    RecreateFormattedText));

        public static readonly DependencyProperty FontSizeProperty =
            TextElement.FontSizeProperty.AddOwner(
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    12.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                    RecreateFormattedText));

        public static readonly DependencyProperty FontStretchProperty =
            TextElement.FontStretchProperty.AddOwner(
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    new FontStretch(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                    RecreateFormattedText));

        public static readonly DependencyProperty FontStyleProperty =
            TextElement.FontStyleProperty.AddOwner(
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    FontStyles.Normal,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                    RecreateFormattedText));

        public static readonly DependencyProperty FontWeightProperty =
            TextElement.FontWeightProperty.AddOwner(
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    FontWeights.Normal,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                    RecreateFormattedText));

        public static readonly DependencyProperty ForegroundProperty =
            TextElement.ForegroundProperty.AddOwner(
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.Black),
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender | FrameworkPropertyMetadataOptions.Inherits,
                    RecreateFormattedText));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsMeasure,
                    RecreateFormattedText));

        private FormattedText formattedText;

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public FontFamily FontFamily 
        {
            get { return (FontFamily)this.GetValue(FontFamilyProperty); }
            set { this.SetValue(FontFamilyProperty, value); }
        }

        public double FontSize 
        { 
            get { return (double)this.GetValue(FontSizeProperty); }
            set { this.SetValue(FontSizeProperty, value); }
        }

        public FontStretch FontStretch
        {
            get { return (FontStretch)this.GetValue(FontStretchProperty); }
            set { this.SetValue(FontStretchProperty, value); }
        }

        public FontStyle FontStyle
        {
            get { return (FontStyle)this.GetValue(FontStyleProperty); }
            set { this.SetValue(FontStyleProperty, value); }
        }

        public FontWeight FontWeight
        {
            get { return (FontWeight)this.GetValue(FontWeightProperty); }
            set { this.SetValue(FontWeightProperty, value); }
        }

        public Brush Foreground
        {
            get { return (Brush)this.GetValue(ForegroundProperty); }
            set { this.SetValue(ForegroundProperty, value); }
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

        private static void RecreateFormattedText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextBlock)d).formattedText = null;
        }

        private FormattedText CreateFormattedText()
        {
            return new FormattedText(
                this.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch),
                this.FontSize,
                this.Foreground);
        }
    }
}
