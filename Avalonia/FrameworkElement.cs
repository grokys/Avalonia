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

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right,
        Stretch,
    }

    public enum VerticalAlignment
    {
        Top = 0,
        Center = 1,
        Bottom = 2,
        Stretch = 3,
    }

    [RuntimeNameProperty("Name")]
    public class FrameworkElement : UIElement, ISupportInitialize
    {
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.Register(
                "DataContext",
                typeof(object),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty DefaultStyleKeyProperty =
            DependencyProperty.Register(
                "DefaultStyleKey",
                typeof(object),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register(
                "HorizontalAlignment",
                typeof(HorizontalAlignment),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    HorizontalAlignment.Stretch,
                    FrameworkPropertyMetadataOptions.AffectsArrange));

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

        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.Register(
                "VerticalAlignment",
                typeof(VerticalAlignment),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    VerticalAlignment.Stretch,
                    FrameworkPropertyMetadataOptions.AffectsArrange));

        internal static readonly DependencyProperty TemplatedParentProperty =
            DependencyProperty.Register(
                "TemplatedParent",
                typeof(DependencyObject),
                typeof(FrameworkElement),
                new PropertyMetadata(TemplatedParentChanged));

        private bool isInitialized;

        public FrameworkElement()
        {
            this.Resources = new ResourceDictionary();
        }

        public event EventHandler Initialized;

        public double ActualWidth
        {
            get { return this.RenderSize.Width; }
        }

        public double ActualHeight
        {
            get { return this.RenderSize.Height; }
        }

        public object DataContext 
        {
            get { return this.GetValue(DataContextProperty); }
            set { this.SetValue(DataContextProperty, value); }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)this.GetValue(HorizontalAlignmentProperty); }
            set { this.SetValue(HorizontalAlignmentProperty, value); }
        }

        public bool IsInitialized
        { 
            get
            {
                return this.isInitialized;
            }

            internal set
            {
                this.isInitialized = value;

                if (this.isInitialized)
                {
                    this.OnInitialized(EventArgs.Empty);
                }
            }
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

        public VerticalAlignment VerticalAlignment
        {
            get { return (VerticalAlignment)this.GetValue(VerticalAlignmentProperty); }
            set { this.SetValue(VerticalAlignmentProperty, value); }
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
            // NOTE: this isn't virtual in WPF, but the Template property isn't defined until 
            // Control so I don't see how it is applied at this level. Making it virtual makes 
            // the most sense for now.
            return false;
        }

        public object FindName(string name)
        {
            INameScope nameScope = this.FindNameScope(this);
            return (nameScope != null) ? nameScope.FindName(name) : null;
        }

        public object FindResource(object resourceKey)
        {
            object resource = this.TryFindResource(resourceKey);

            if (resource != null)
            {
                return resource;
            }
            else
            {
                throw new ResourceReferenceKeyNotFoundException(
                    string.Format("'{0}' resource not found", resourceKey),
                    resourceKey);
            }
        }

        public virtual void OnApplyTemplate()
        {
        }

        public object TryFindResource(object resourceKey)
        {
            FrameworkElement element = this;
            object resource = null;

            while (resource == null && element != null)
            {
                resource = element.Resources[resourceKey];
                element = (FrameworkElement)VisualTreeHelper.GetParent(element);
            }

            if (resource == null && Application.Current != null)
            {
                resource = Application.Current.Resources[resourceKey];
            }

            if (resource == null)
            {
                resource = Application.GenericTheme[resourceKey];
            }

            return resource;
        }

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
            this.IsInitialized = true;
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

                this.InvalidateMeasure();
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
                this.InvalidateMeasure();
            }
        }

        protected internal virtual DependencyObject GetTemplateChild(string childName)
        {
            return null;
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

            if (this.HorizontalAlignment != HorizontalAlignment.Stretch)
            {
                size = new Size(Math.Min(size.Width, this.DesiredSize.Width), size.Height);
            }

            if (this.VerticalAlignment != VerticalAlignment.Stretch)
            {
                size = new Size(size.Width, Math.Min(size.Height, this.DesiredSize.Height));
            }

            size = this.ArrangeOverride(size);

            switch (this.HorizontalAlignment)
            {
                case HorizontalAlignment.Center:
                    origin = new Point((finalRect.Right - size.Width) / 2, origin.Y);
                    break;
                case HorizontalAlignment.Right:
                    origin = new Point(finalRect.Right - size.Width, origin.Y);
                    break;
            }

            switch (this.VerticalAlignment)
            {
                case VerticalAlignment.Center:
                    origin = new Point(origin.X, (finalRect.Bottom - size.Height) / 2);
                    break;
                case VerticalAlignment.Bottom:
                    origin = new Point(origin.X, finalRect.Bottom - size.Height);
                    break;
            }

            base.ArrangeCore(new Rect(origin, size));
        }

        protected virtual Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }

        protected virtual void OnInitialized(EventArgs e)
        {
            if (this.Initialized != null)
            {
                this.Initialized(this, e);
            }
        }

        protected internal override void OnVisualParentChanged(DependencyObject oldParent)
        {
            if (this.VisualParent != null)
            {
                this.IsInitialized = true;
            }
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
