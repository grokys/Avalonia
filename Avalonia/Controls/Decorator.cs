// -----------------------------------------------------------------------
// <copyright file="Decorator.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Windows.Markup;
    using Avalonia.Media;

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

                this.InvalidateMeasure();
            }
        }

        protected internal override IEnumerator LogicalChildren
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

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.child != null)
            {
                this.child.Measure(constraint);
                return this.child.DesiredSize;
            }

            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.child != null)
            {
                this.child.Arrange(new Rect(finalSize));
                return finalSize;
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}
