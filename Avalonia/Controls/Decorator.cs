// -----------------------------------------------------------------------
// <copyright file="Decorator.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Windows.Markup;
    using Avalonia.Media;
    using System.Linq;

    [ContentProperty("Child")]
    public class Decorator : FrameworkElement
    {
        private UIElement child;

        public virtual UIElement Child
        {
            get
            {
                return this.child;
            }

            set
            {
                if (this.child != null)
                {
                    this.RemoveVisualChild(this.child);
                    this.RemoveLogicalChild(this.child);
                }

                this.child = value;

                if (this.child != null)
                {
                    this.AddVisualChild(this.child);
                    this.AddLogicalChild(this.child);
                }
            }
        }

        protected internal override int VisualChildrenCount
        {
            get { return (this.Child != null) ? 1 : 0; }
        }

        protected internal override Visual GetVisualChild(int index)
        {
            if (index > 0 || this.Child == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.Child;
        }

        protected internal override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                if (this.child != null)
                {
                    return Enumerable.Repeat<object>(this.child, 1).GetEnumerator();
                }
                else
                {
                    return Enumerable.Empty<object>().GetEnumerator();
                }
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.VisualChildrenCount > 0)
            {
                UIElement ui = this.GetVisualChild(0) as UIElement;

                if (ui != null)
                {
                    ui.Measure(constraint);
                    return ui.DesiredSize;
                }
            }

            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.VisualChildrenCount > 0)
            {
                UIElement ui = this.GetVisualChild(0) as UIElement;

                if (ui != null)
                {
                    ui.Arrange(new Rect(finalSize));
                    return finalSize;
                }
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}
