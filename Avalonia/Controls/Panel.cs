// -----------------------------------------------------------------------
// <copyright file="Panel.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Windows.Markup;
    using Avalonia.Media;

    [ContentProperty("Children")]
    public abstract class Panel : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush),
                typeof(Panel),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.White),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public Panel()
        {
            this.Children = new UIElementCollection(this, this);
        }

        public UIElementCollection Children
        { 
            get; 
            private set; 
        }

        protected internal override IEnumerator LogicalChildren
        {
            get { return this.Children.GetEnumerator(); }
        }

        protected internal UIElementCollection InternalChildren
        {
            get { return this.Children; }
        }
        
        protected internal override int VisualChildrenCount
        {
            get { return this.Children.Count; }
        }

        protected internal override Visual GetVisualChild(int index)
        {
            return this.InternalChildren[index];
        }
    }
}
