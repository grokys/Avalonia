// -----------------------------------------------------------------------
// <copyright file="Border.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Media;

    public class Border : Decorator
    {
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background", 
                typeof(Brush), 
                typeof(Border),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.White),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public CornerRadius CornerRadius { get; set; }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(new Point(), new Size(this.ActualWidth, this.ActualHeight));

            if (this.CornerRadius.TopLeft > 0 || this.CornerRadius.BottomLeft > 0)
            {
                drawingContext.DrawRoundedRectangle(
                    this.Background,
                    null,
                    rect,
                    this.CornerRadius.TopLeft,
                    this.CornerRadius.BottomLeft);
            }
            else
            {
                drawingContext.DrawRectangle(
                    this.Background,
                    null,
                    rect);
            }
        }
    }
}
