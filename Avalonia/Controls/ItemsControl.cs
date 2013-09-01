// -----------------------------------------------------------------------
// <copyright file="ItemsControl.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Markup;
    using Avalonia.Data;
    using Avalonia.Media;

    [ContentProperty("Items")]
    public class ItemsControl : Control
    {
        public static readonly DependencyProperty ItemsPanelProperty = 
            DependencyProperty.Register(
                "ItemsPanel",
                typeof(ItemsPanelTemplate),
                typeof(ItemsControl),
                new FrameworkPropertyMetadata(
                    new ItemsPanelTemplate(
                        new FrameworkElementFactory(typeof(StackPanel)))));

        static ItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsControl), new FrameworkPropertyMetadata(typeof(ItemsControl)));
        }

        public ItemsControl()
        {
            this.Items = new ItemCollection();
        }

        [Bindable(true)]
        public ItemCollection Items 
        { 
            get; 
            private set; 
        }

        [BindableAttribute(false)]
        public ItemsPanelTemplate ItemsPanel 
        {
            get { return (ItemsPanelTemplate)this.GetValue(ItemsPanelProperty); }
            set { this.SetValue(ItemsPanelProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ItemsPresenter itemsPresenter = VisualTreeHelper.GetDescendents<ItemsPresenter>(this).First();
            Panel panel = (Panel)this.ItemsPanel.CreateVisualTree(itemsPresenter);

            foreach (object item in this.Items)
            {
                ContentPresenter p = new ContentPresenter();
                p.DataContext = item;
                p.SetBinding(ContentPresenter.ContentProperty, new Binding());
                panel.InternalChildren.Add(p);
            }

            itemsPresenter.Child = panel;
        }
    }
}
