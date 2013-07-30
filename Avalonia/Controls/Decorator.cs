namespace Avalonia.Controls
{
    using System;
    using Avalonia.Media;

    public class Decorator : FrameworkElement
    {
        public virtual UIElement Child { get; set; }

        protected internal override int VisualChildrenCount
        {
            get { return (Child != null) ? 1 : 0; }
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
            if (this.VisualChildrenCount > 0)
            {
                UIElement uiElement = this.GetVisualChild(0) as UIElement;

                if (uiElement != null)
                {
                    uiElement.Measure(constraint);
                    return uiElement.DesiredSize;
                }
            }

            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.VisualChildrenCount > 0)
            {
                UIElement uiElement = this.GetVisualChild(0) as UIElement;

                if (uiElement != null)
                {
                    uiElement.Arrange(new Rect(finalSize));
                    return finalSize;
                }
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}
