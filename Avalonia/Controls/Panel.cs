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

        private bool isItemsPanel;

        public Panel()
        {
            this.InternalChildren = new UIElementCollection(this, this);
        }

        public UIElementCollection Children
        { 
            get { return this.isItemsPanel ? null : this.InternalChildren; }
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
            get { return this.Children.Count; }
        }

        internal void MakeItemsPanel()
        {
            this.isItemsPanel = true;
        }

        protected internal override Visual GetVisualChild(int index)
        {
            return this.InternalChildren[index];
        }
    }
}
