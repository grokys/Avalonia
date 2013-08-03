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
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Markup;
    using Avalonia.Media;

    [RuntimeNameProperty("Name")]
    public class FrameworkElement : UIElement
    {
        public static readonly DependencyProperty DefaultStyleKeyProperty =
            DependencyProperty.Register(
                "DefaultStyleKey",
                typeof(object),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.Register(
                "Margin",
                typeof(Thickness),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    new Thickness(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty StyleProperty =
            DependencyProperty.Register(
                "Style",
                typeof(Style),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    null, 
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    StyleChanged));

        internal static readonly DependencyProperty TemplatedParentProperty =
            DependencyProperty.Register(
                "TemplatedParent",
                typeof(DependencyObject),
                typeof(FrameworkElement),
                new PropertyMetadata(TemplatedParentChanged));

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

        public Style Style 
        {
            get { return (Style)this.GetValue(StyleProperty); }
            set { this.SetValue(StyleProperty, value); }
        }

        public DependencyObject TemplatedParent
        {
            get { return (DependencyObject)this.GetValue(TemplatedParentProperty); }
            internal set { this.SetValue(TemplatedParentProperty, value); }
        }

        protected internal object DefaultStyleKey
        {
            get { return this.GetValue(DefaultStyleKeyProperty); }
            set { this.SetValue(DefaultStyleKeyProperty, value); }
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
            INameScope nameScope = this.FindNameScope(this);
            return (nameScope != null) ? nameScope.FindName(name) : null;
        }

        public object FindResource(object resourceKey)
        {
            FrameworkElement element = this;
            object resource;

            while (element != null)
            {
                resource = element.Resources[resourceKey];

                if (resource != null)
                {
                    return resource;
                }

                element = (FrameworkElement)LogicalTreeHelper.GetParent(element);
            }

            return Application.Current.FindResource(resourceKey);
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

                if (this.TemplatedParent != null)
                {
                    this.PropagateTemplatedParent(fe, this.TemplatedParent);
                }
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

        protected internal virtual void OnStyleChanged(Style oldStyle, Style newStyle)
        {
            if (oldStyle != null)
            {
                oldStyle.Detach(this);
            }

            if (newStyle != null)
            {
                newStyle.Attach(this);
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

        private static void StyleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((FrameworkElement)sender).OnStyleChanged((Style)e.OldValue, (Style)e.NewValue);
        }

        private static void TemplatedParentChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.PropagateTemplatedParent(element, element.TemplatedParent);
        }

        private INameScope FindNameScope(FrameworkElement e)
        {
            while (e != null)
            {
                INameScope nameScope = e as INameScope ?? NameScope.GetNameScope(e);

                if (nameScope != null)
                {
                    return nameScope;
                }

                e = LogicalTreeHelper.GetParent(e) as FrameworkElement;
            }

            return null;
        }

        private void PropagateTemplatedParent(FrameworkElement element, DependencyObject templatedParent)
        {
            element.TemplatedParent = templatedParent;

            foreach (FrameworkElement child in LogicalTreeHelper.GetChildren(element).OfType<FrameworkElement>())
            {
                child.TemplatedParent = templatedParent;
            }
        }
    }
}
