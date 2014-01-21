// -----------------------------------------------------------------------
// <copyright file="Selector.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    [DefaultEvent("SelectionChanged")]
    [DefaultProperty("SelectedIndex")]
    public abstract class Selector : ItemsControl
    {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached(
                "IsSelected",
                typeof(bool),
                typeof(Selector));

        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty =
            DependencyProperty.Register(
                "IsSynchronizedWithCurrentItem",
                typeof(bool?),
                typeof(Selector));

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                "SelectedIndex",
                typeof(int),
                typeof(Selector),
                new PropertyMetadata(-1, SelectedIndexChanged, CoerceSelectedIndex));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(Selector),
                new PropertyMetadata(null, SelectedItemChanged, CoerceSelectedItem));

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register(
                "SelectedValue",
                typeof(object),
                typeof(Selector));

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register(
                "SelectedValuePath",
                typeof(string),
                typeof(Selector));

        public static readonly RoutedEvent SelectionChangedEvent =
            EventManager.RegisterRoutedEvent(
                "SelectionChanged",
                RoutingStrategy.Bubble,
                typeof(SelectionChangedEventHandler),
                typeof(Selector));

        public static readonly RoutedEvent UnselectedEvent;

        protected Selector()
        {
        }

        public event SelectionChangedEventHandler SelectionChanged
        {
            add { this.AddHandler(SelectionChangedEvent, value); }
            remove { this.AddHandler(SelectionChangedEvent, value); }
        }

        public bool? IsSynchronizedWithCurrentItem 
        {
            get { return (bool?)this.GetValue(IsSynchronizedWithCurrentItemProperty); }
            set { this.SetValue(IsSynchronizedWithCurrentItemProperty, value); }
        }

        public int SelectedIndex 
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { this.SetValue(SelectedIndexProperty, value); }
        }

        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        public object SelectedValue
        {
            get { return this.GetValue(SelectedValueProperty); }
            set { this.SetValue(SelectedValueProperty, value); }
        }

        public string SelectedValuePath
        {
            get { return (string)this.GetValue(SelectedValuePathProperty); }
            set { this.SetValue(SelectedValuePathProperty, value); }
        }

        public static bool GetIsSelected(DependencyObject element)
        {
            return (bool)element.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject element, bool isSelected)
        {
            element.SetValue(IsSelectedProperty, isSelected);
        }

        protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            this.RaiseEvent(e);
        }

        private static object CoerceSelectedIndex(DependencyObject d, object baseValue)
        {
            Selector s = (Selector)d;
            int value = (int)baseValue;
            return value < s.Items.Count ? value : s.SelectedIndex;
        }

        private static object CoerceSelectedItem(DependencyObject d, object baseValue)
        {
            Selector s = (Selector)d;
            return (baseValue == null || s.Items.Contains(baseValue)) ? 
                baseValue : s.SelectedItem;
        }

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Selector)d).OnSelectedIndexChanged();
        }

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Selector)d).OnSelectedItemChanged();
        }

        private object GetSelectedValue(object o)
        {
            string selectedValuePath = this.SelectedValuePath;

            if (string.IsNullOrWhiteSpace(selectedValuePath))
            {
                return o;
            }
            else
            {
                PropertyInfo p = o.GetType().GetProperty(selectedValuePath);
                return (p != null) ? p.GetValue(o) : null;
            }
        }

        private void OnSelectedIndexChanged()
        {
            int selectedIndex = this.SelectedIndex;

            if (selectedIndex < -1)
            {
                throw new ArgumentException("SelectedIndex is out of range.");
            }

            if (selectedIndex == -1)
            {
                this.ClearValue(SelectedItemProperty);
                this.ClearValue(SelectedValueProperty);
            }
            else
            {
                this.SelectedItem = this.Items[selectedIndex];
                this.SelectedValue = this.GetSelectedValue(this.SelectedItem);
            }
        }

        private void OnSelectedItemChanged()
        {
            object selectedItem = this.SelectedItem;

            if (selectedItem == null)
            {
                this.ClearValue(SelectedIndexProperty);
                this.ClearValue(SelectedValueProperty);
            }
            else
            {
                this.SelectedIndex = this.Items.IndexOf(selectedItem);
                this.SelectedValue = this.GetSelectedValue(selectedItem);
            }
        }
    }
}
