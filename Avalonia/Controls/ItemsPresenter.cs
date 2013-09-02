// -----------------------------------------------------------------------
// <copyright file="ItemsPresenter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using Avalonia.Media;

    public class ItemsPresenter : FrameworkElement
    {
        private Panel child;

        public Panel Child
        {
            get
            {
                return this.child;
            }

            set
            {
                if (this.child != value)
                {
                    if (this.child != null)
                    {
                        this.RemoveVisualChild(this.child);
                    }

                    this.child = value;

                    if (this.child != null)
                    {
                        this.AddVisualChild(this.child);
                    }
                }
            }
        }

        protected internal override int VisualChildrenCount
        {
            get { return (this.child != null) ? 1 : 0; }
        }

        protected internal override Visual GetVisualChild(int index)
        {
            if (index > 0 || this.child == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.child;
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
