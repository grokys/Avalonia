// -----------------------------------------------------------------------
// <copyright file="ContentPresenter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Media;

    public class ContentPresenter : FrameworkElement
    {
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                "Content",
                typeof(object),
                typeof(ContentPresenter),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    ContentChanged));

        public static readonly DependencyProperty ContentTemplateProperty =
            ContentControl.ContentTemplateProperty.AddOwner(
                typeof(ContentPresenter),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        private Visual visualChild;

        public ContentPresenter()
        {
        }

        internal ContentPresenter(ContentControl templatedParent)
        {
            this.Content = templatedParent.Content;
        }

        public object Content 
        {
            get { return this.GetValue(ContentProperty); }
            set { this.SetValue(ContentProperty, value); }
        }

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)this.GetValue(ContentTemplateProperty); }
            set { this.SetValue(ContentTemplateProperty, value); }
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

        protected internal override void OnVisualParentChanged(DependencyObject oldParent)
        {
            this.visualChild = this.ApplyDataTemplate();
            base.OnVisualParentChanged(oldParent);
        }

        private static void ContentChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((ContentPresenter)sender).ContentChanged(e.OldValue, e.NewValue);
        }

        private Visual ApplyDataTemplate()
        {
            Visual visual = this.Content as Visual;

            if (visual == null && this.Content != null)
            {
                DataTemplate template = this.ContentTemplate;

                if (template == null)
                {
                    DataTemplateKey key = new DataTemplateKey(this.Content.GetType());
                    template = this.TryFindResource(key) as DataTemplate;
                }

                if (template != null)
                {
                    visual = template.CreateVisualTree(this);

                    FrameworkElement fe = visual as FrameworkElement;

                    if (fe != null)
                    {
                        fe.DataContext = this.Content;
                    }
                }
                else
                {
                    visual = new TextBlock
                    {
                        Text = this.Content.ToString(),
                    };
                }
            }

            return visual;
        }

        private void ContentChanged(object oldValue, object newValue)
        {
            if (oldValue != null)
            {
                this.RemoveLogicalChild(oldValue);
                this.RemoveVisualChild(this.visualChild);
            }

            this.visualChild = null;

            if (newValue != null)
            {
                this.visualChild = this.ApplyDataTemplate();
                this.AddLogicalChild(newValue);
                this.AddVisualChild(this.visualChild);
            }
        }
    }
}
