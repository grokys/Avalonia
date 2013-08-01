// -----------------------------------------------------------------------
// <copyright file="FrameworkElement.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Markup;
    using Avalonia.Media;

    public class FrameworkElement : UIElement
    {
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.Register(
                "Margin",
                typeof(Thickness),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    new Thickness(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkElement"/> class.
        /// </summary>
        public FrameworkElement()
        {
            this.Resources = new ResourceDictionary();
        }

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
            get { return (Thickness)this.GetValue(MarginProperty); }
            set { this.SetValue(MarginProperty, value); }
        }

        public string Name
        {
            get;
            set;
        }

        public DependencyObject Parent
        {
            get;
            private set;
        }

        [Ambient]
        public ResourceDictionary Resources
        {
            get;
            set;
        }

        public DependencyObject TemplatedParent
        {
            get;
            internal set;
        }

        protected internal virtual IEnumerator LogicalChildren
        {
            get { return new object[0].GetEnumerator(); }
        }

        public virtual bool ApplyTemplate()
        {
            return false;
        }

        public object FindName(string name)
        {
            throw new NotImplementedException();
            //// INameScope nameScope = this.FindNameScope(this);
            //// return (nameScope != null) ? nameScope.FindName(name) : null;
        }

        protected internal void AddLogicalChild(object child)
        {
            FrameworkElement fe = child as FrameworkElement;

            if (fe != null)
            {
                if (fe.Parent != null)
                {
                    throw new InvalidOperationException("FrameworkElement already has a parent.");
                }

                fe.Parent = this;
            }
        }

        protected internal void RemoveLogicalChild(object child)
        {
            FrameworkElement fe = child as FrameworkElement;

            if (fe != null)
            {
                if (fe.Parent != this)
                {
                    throw new InvalidOperationException("FrameworkElement is not a child of this object.");
                }

                fe.Parent = null;
            }
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

            size = this.ArrangeOverride(size);
            base.ArrangeCore(new Rect(origin, size));
        }

        protected virtual Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }
    }
}
