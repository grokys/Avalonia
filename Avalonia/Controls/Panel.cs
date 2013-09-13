// -----------------------------------------------------------------------
// <copyright file="Panel.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
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

        public static readonly DependencyProperty IsItemsHostProperty =
            DependencyProperty.Register(
                "IsItemsHost",
                typeof(bool),
                typeof(Panel));
        
        public Panel()
        {
            this.InternalChildren = new UIElementCollection(this, this);
        }

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public UIElementCollection Children
        { 
            get { return this.IsItemsHost ? null : this.InternalChildren; }
        }

        [Bindable(false)]
        public bool IsItemsHost 
        {
            get { return (bool)this.GetValue(IsItemsHostProperty); }
            set { this.SetValue(IsItemsHostProperty, value); }
        }
        
        protected internal override IEnumerator LogicalChildren
        {
            get { return this.InternalChildren.GetEnumerator(); }
        }

        protected internal UIElementCollection InternalChildren
        {
            get;
            private set;
        }

        protected internal override int VisualChildrenCount
        {
            get { return this.InternalChildren.Count; }
        }

        protected internal override Visual GetVisualChild(int index)
        {
            return this.InternalChildren[index];
        }
    }
}
