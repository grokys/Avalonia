// -----------------------------------------------------------------------
// <copyright file="Control.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using Avalonia.Media;

    public class Control : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush),
                typeof(Control),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.White),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.Register(
                "Template",
                typeof(ControlTemplate),
                typeof(Control),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        private bool templateApplied;
        private List<Visual> visualChildren;

        public Control()
        {
            this.visualChildren = new List<Visual>();
            this.Background = new SolidColorBrush(Colors.White);
        }

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public ControlTemplate Template 
        {
            get { return (ControlTemplate)this.GetValue(TemplateProperty); }
            set { this.SetValue(TemplateProperty, value); }
        }

        protected internal override int VisualChildrenCount
        {
            get { return this.visualChildren.Count; }
        }

        public override bool ApplyTemplate()
        {
            if (!this.templateApplied)
            {
                this.templateApplied = true;

                if (this.Template == null)
                {
                    this.ApplyTheme();
                }

                if (this.Template != null)
                {
                    FrameworkElement child = this.Template.CreateVisualTree(this);
                    this.visualChildren.Add(child);
                    this.AddVisualChild(child);
                    return true;
                }
            }

            return false;
        }

        protected internal override Visual GetVisualChild(int index)
        {
            return this.visualChildren[index];
        }

        private void ApplyTheme()
        {
            object defaultStyleKey = this.DefaultStyleKey;

            if (defaultStyleKey == null)
            {
                throw new InvalidOperationException("DefaultStyleKey must be set.");
            }

            Style style = (Style)this.FindResource(this.DefaultStyleKey);
            style.Attach(this);
        }
    }
}
