// -----------------------------------------------------------------------
// <copyright file="ContentControl.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Linq;

    public class ContentControl : Control
    {
        public object Content { get; set; }

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
