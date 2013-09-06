// -----------------------------------------------------------------------
// <copyright file="Image.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Windows.Markup;
    using Avalonia.Media;

    public enum StretchDirection
    {
        UpOnly,
        DownOnly,
        Both,
    }

    public class Image : FrameworkElement, IUriContext
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                "Source",
                typeof(ImageSource),
                typeof(Image),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(
                "Stretch",
                typeof(Stretch),
                typeof(Image),
                new FrameworkPropertyMetadata(
                    Stretch.Uniform,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty StretchDirectionProperty =
            DependencyProperty.Register(
                "StretchDirection",
                typeof(StretchDirection),
                typeof(Image),
                new FrameworkPropertyMetadata(
                    StretchDirection.Both,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public ImageSource Source
        {
            get { return (ImageSource)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

        public Stretch Stretch 
        { 
            get { return (Stretch)this.GetValue(StretchProperty); }
            set { this.SetValue(StretchProperty, value); }
        }

        public StretchDirection StretchDirection
        {
            get { return (StretchDirection)this.GetValue(StretchDirectionProperty); }
            set { this.SetValue(StretchDirectionProperty, value); }
        }

        Uri IUriContext.BaseUri
        {
            get { return this.BaseUri; }
            set { this.BaseUri = value; }
        }

        protected virtual Uri BaseUri 
        { 
            get; 
            set; 
        }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            if (this.Source != null)
            {
                drawingContext.DrawImage(this.Source, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            }
        }
    }
}
