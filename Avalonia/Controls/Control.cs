// -----------------------------------------------------------------------
// <copyright file="Control.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System.Collections.Generic;
    using Avalonia.Media;

    public class Control : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush),
                typeof(Control),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.White),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        private List<Visual> visualChildren;

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        public Control()
        {
            this.visualChildren = new List<Visual>();
            this.Background = new SolidColorBrush(Colors.White);
        }

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public ControlTemplate Template { get; set; }

        protected internal override int VisualChildrenCount
        {
            get { return this.visualChildren.Count; }
        }

        public override bool ApplyTemplate()
        {
            if (this.visualChildren.Count == 0)
            {
                Visual child = this.Template.CreateVisualTree(this);
                this.visualChildren.Add(child);
                this.AddVisualChild(child);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected internal override Visual GetVisualChild(int index)
        {
            return this.visualChildren[index];
        }
    }
}
