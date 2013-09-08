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
    using Avalonia.Media.Imaging;

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
            BitmapSource source = this.Source as BitmapSource;

            if (source != null)
            {
                Rect sourceRect = new Rect(0, 0, source.PixelWidth, source.PixelHeight);
                Rect destRect = new Rect(this.RenderSize);

                switch (this.Stretch)
                {
                    case Stretch.None:
                        sourceRect = new Rect(
                            0,
                            0,
                            Math.Min(this.ActualWidth, source.PixelWidth),
                            Math.Min(this.ActualHeight, source.PixelHeight));
                        break;

                    case Stretch.Uniform:
                    {
                        double scale = Math.Min(
                            this.DesiredSize.Width / source.PixelWidth, 
                            this.DesiredSize.Height / source.PixelHeight);
                        double scaledWidth = source.PixelWidth * scale;
                        double scaledHeight = source.PixelHeight * scale;
                        destRect = new Rect(
                            (this.ActualWidth - scaledWidth) / 2,
                            (this.ActualHeight - scaledHeight) / 2,
                            scaledWidth,
                            scaledHeight);
                        break;
                    }

                    case Stretch.UniformToFill:
                    {
                        double scale = Math.Max(
                            this.DesiredSize.Width / source.PixelWidth,
                            this.DesiredSize.Height / source.PixelHeight);
                        sourceRect = new Rect(
                            0,
                            0,
                            this.ActualWidth / scale,
                            this.ActualHeight / scale);
                        break;
                    }
                }

                drawingContext.DrawImage(source, 1, sourceRect, destRect);
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size desired = constraint;
            Rect shapeBounds = new Rect();
            BitmapSource source = this.Source as BitmapSource;
            double sx = 0.0;
            double sy = 0.0;

            if (source != null)
            {
                shapeBounds = new Rect(0, 0, source.PixelWidth, source.PixelHeight);
            }

            if (double.IsInfinity(desired.Width))
            {
                desired.Width = shapeBounds.Width;
            }

            if (double.IsInfinity(desired.Height))
            {
                desired.Height = shapeBounds.Height;
            }

            if (shapeBounds.Width > 0)
            {
                sx = desired.Width / shapeBounds.Width;
            }

            if (shapeBounds.Height > 0)
            {
                sy = desired.Height / shapeBounds.Height;
            }

            if (double.IsInfinity(constraint.Width))
            {
                sx = sy;
            }

            if (double.IsInfinity(constraint.Height))
            {
                sy = sx;
            }

            switch (this.Stretch)
            {
                case Stretch.Uniform:
                    sx = sy = Math.Min(sx, sy);
                    break;
                case Stretch.UniformToFill:
                    sx = sy = Math.Max(sx, sy);
                    break;
                case Stretch.Fill:
                    if (double.IsInfinity(constraint.Width))
                    {
                        sx = sy;
                    }

                    if (double.IsInfinity(constraint.Height))
                    {
                        sy = sx;
                    }

                    break;
                case Stretch.None:
                    sx = sy = 1;
                    break;
            }

            desired = new Size(shapeBounds.Width * sx, shapeBounds.Height * sy);
            return desired;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }
    }
}
