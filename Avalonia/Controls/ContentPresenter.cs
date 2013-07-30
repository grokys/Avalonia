using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace Avalonia.Controls
{
    public class ContentPresenter : FrameworkElement
    {
        Visual visualChild;

        public ContentPresenter()
        {
        }

        internal ContentPresenter(ContentControl templatedParent)
        {
            this.Content = templatedParent.Content;
        }

        public Object Content { get; set; }

        public override bool ApplyTemplate()
        {
            this.visualChild = Content as Visual;
            return true;
        }

        protected internal override int VisualChildrenCount
        {
            get { return (this.visualChild != null) ? 1 : 0; }
        }

        protected internal override Visual GetVisualChild(int index)
        {
            if (index > 0 || this.visualChild == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.visualChild;
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
