using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace Avalonia
{
    public class FrameworkElement : UIElement
    {
        public double ActualWidth 
        {
            get { return this.RenderSize.Width; }
        }

        public double ActualHeight
        {
            get { return this.RenderSize.Height; }
        }

        public Thickness Margin 
        { 
            get; 
            set; 
        }

        public DependencyObject TemplatedParent 
        { 
            get; 
            internal set; 
        }

        public virtual bool ApplyTemplate()
        {
            return false;
        }

        protected sealed override Size MeasureCore(Size availableSize)
        {
            this.ApplyTemplate();

            availableSize = new Size(
                Math.Max(0, availableSize.Width - this.Margin.Left - this.Margin.Right),
                Math.Max(0, availableSize.Height - this.Margin.Top - this.Margin.Bottom));
            
            return this.MeasureOverride(availableSize);
        }

        protected virtual Size MeasureOverride(Size constraint)
        {
            return new Size();
        }

        protected sealed override void ArrangeCore(Rect finalRect)
        {
            Point origin = new Point(
                finalRect.Left + this.Margin.Left, 
                finalRect.Top + this.Margin.Top);
            Size size = new Size(
                Math.Max(0, finalRect.Width - this.Margin.Left - this.Margin.Right), 
                Math.Max(0, finalRect.Height - this.Margin.Top - this.Margin.Bottom));

            size = ArrangeOverride(size);
            base.ArrangeCore(new Rect(origin, size));
        }

        protected virtual Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }
    }
}
