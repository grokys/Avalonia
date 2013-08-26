// -----------------------------------------------------------------------
// <copyright file="Control.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using Avalonia.Document;
using Avalonia.Input;
using Avalonia.Media;

    public class Control : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty =
            Panel.BackgroundProperty.AddOwner(
                typeof(Control),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.White),
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(
                typeof(Control),
                new FrameworkPropertyMetadata(
                    new FontFamily("Segoe UI"),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontSizeProperty =
            TextElement.FontSizeProperty.AddOwner(
                typeof(Control),
                new FrameworkPropertyMetadata(
                    12.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontStretchProperty =
            TextElement.FontStretchProperty.AddOwner(
                typeof(Control),
                new FrameworkPropertyMetadata(
                    new FontStretch(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontStyleProperty =
            TextElement.FontStyleProperty.AddOwner(
                typeof(Control),
                new FrameworkPropertyMetadata(
                    FontStyles.Normal,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontWeightProperty =
            TextElement.FontWeightProperty.AddOwner(
                typeof(Control),
                new FrameworkPropertyMetadata(
                    FontWeights.Normal,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ForegroundProperty =
            TextElement.ForegroundProperty.AddOwner(
                typeof(Control),
                new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.Black),
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register(
                "HorizontalContentAlignment",
                typeof(HorizontalAlignment),
                typeof(Control),
                new FrameworkPropertyMetadata(HorizontalAlignment.Left));

        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.Register(
                "Template",
                typeof(ControlTemplate),
                typeof(Control),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register(
                "VerticalContentAlignment",
                typeof(VerticalAlignment),
                typeof(Control),
                new FrameworkPropertyMetadata(VerticalAlignment.Top));

        private Visual child;

        static Control()
        {
            FocusableProperty.OverrideMetadata(typeof(Control), new PropertyMetadata(true));
        }

        public Control()
        {
            this.Background = new SolidColorBrush(Colors.White);

            this.AddHandler(KeyDownEvent, (KeyEventHandler)((s, e) => this.OnKeyDown(e)));
        }

        public Brush Background
        {
            get { return (Brush)this.GetValue(BackgroundProperty); }
            set { this.SetValue(BackgroundProperty, value); }
        }

        public FontFamily FontFamily
        {
            get { return (FontFamily)this.GetValue(FontFamilyProperty); }
            set { this.SetValue(FontFamilyProperty, value); }
        }

        public double FontSize
        {
            get { return (double)this.GetValue(FontSizeProperty); }
            set { this.SetValue(FontSizeProperty, value); }
        }

        public FontStretch FontStretch
        {
            get { return (FontStretch)this.GetValue(FontStretchProperty); }
            set { this.SetValue(FontStretchProperty, value); }
        }

        public FontStyle FontStyle
        {
            get { return (FontStyle)this.GetValue(FontStyleProperty); }
            set { this.SetValue(FontStyleProperty, value); }
        }

        public FontWeight FontWeight
        {
            get { return (FontWeight)this.GetValue(FontWeightProperty); }
            set { this.SetValue(FontWeightProperty, value); }
        }

        public Brush Foreground
        {
            get { return (Brush)this.GetValue(ForegroundProperty); }
            set { this.SetValue(ForegroundProperty, value); }
        }

        public HorizontalAlignment HorizontalContentAlignment 
        {
            get { return (HorizontalAlignment)this.GetValue(HorizontalContentAlignmentProperty); } 
            set { this.SetValue(HorizontalContentAlignmentProperty, value); } 
        }

        public ControlTemplate Template 
        {
            get { return (ControlTemplate)this.GetValue(TemplateProperty); }
            set { this.SetValue(TemplateProperty, value); }
        }

        public VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)this.GetValue(VerticalContentAlignmentProperty); }
            set { this.SetValue(VerticalContentAlignmentProperty, value); }
        }

        protected internal override int VisualChildrenCount
        {
            get { return (this.child != null) ? 1 : 0; }
        }

        public sealed override bool ApplyTemplate()
        {
            if (this.child == null)
            {
                if (this.Template == null)
                {
                    this.ApplyTheme();
                }

                if (this.Template != null)
                {
                    this.child = this.Template.CreateVisualTree(this);
                    this.AddVisualChild(this.child);
                    this.OnApplyTemplate();
                    return true;
                }
            }

            return false;
        }

        protected internal sealed override DependencyObject GetTemplateChild(string childName)
        {
            if (this.child != null)
            {
                INameScope nameScope = NameScope.GetNameScope(this.child);

                if (nameScope != null)
                {
                    return nameScope.FindName(childName) as DependencyObject;
                }
            }

            return null;
        }

        protected internal override Visual GetVisualChild(int index)
        {
            if (this.child != null && index == 0)
            {
                return this.child;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            UIElement ui = this.child as UIElement;

            if (ui != null)
            {
                ui.Measure(constraint);
                return ui.DesiredSize;
            }

            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            UIElement ui = this.child as UIElement;

            if (ui != null)
            {
                ui.Arrange(new Rect(finalSize));
                return finalSize;
            }

            return base.ArrangeOverride(finalSize);
        }

        protected virtual void OnKeyDown(KeyEventArgs e)
        {
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
