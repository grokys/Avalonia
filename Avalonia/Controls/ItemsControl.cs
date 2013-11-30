// -----------------------------------------------------------------------
// <copyright file="ItemsControl.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Markup;
    using Avalonia.Controls.Primitives;
    using Avalonia.Media;
    using Avalonia.Utils;

    [ContentProperty("Items")]
    public class ItemsControl : Control
    {
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                "DisplayMemberPath", 
                typeof(string), 
                typeof(ItemsControl),
                new PropertyMetadata(null, new PropertyChangedCallback(DisplayMemberPathChanged)));

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(
                "Items", 
                typeof(ItemCollection), 
                typeof(ItemsControl));

        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register(
            "ItemsPanel", 
            typeof(ItemsPanelTemplate), 
            typeof(ItemsControl),
            new PropertyMetadata(new ItemsPanelTemplate(new FrameworkElementFactory(typeof(StackPanel)))));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource", 
                typeof(IEnumerable), 
                typeof(ItemsControl),
                new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourceChanged)));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
                "ItemTemplate", 
                typeof(DataTemplate), 
                typeof(ItemsControl), 
                new PropertyMetadata(ItemTemplateChanged));

        private DataTemplate displayMemberTemplate;

        private bool itemsIsDataBound;

        private ItemsPresenter itemsPresenter;

        public ItemsControl()
        {
            this.DefaultStyleKey = typeof(ItemsControl);
            this.ItemContainerGenerator = new ItemContainerGenerator(this);
            this.ItemContainerGenerator.ItemsChanged += this.OnItemContainerGeneratorChanged;

            ItemCollection items = new ItemCollection();
            ((INotifyCollectionChanged)items).CollectionChanged += this.InvokeItemsChanged;
            this.itemsIsDataBound = false;
            this.SetValue(ItemsProperty, items);
        }

        public ItemCollection Items
        {
            get { return (ItemCollection)this.GetValue(ItemsProperty); }
        }

        public string DisplayMemberPath
        {
            get { return (string)this.GetValue(DisplayMemberPathProperty); }
            set { this.SetValue(DisplayMemberPathProperty, value); }
        }

        public ItemsPanelTemplate ItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
            set { this.SetValue(ItemsPanelProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get 
            { 
                return (IEnumerable)this.GetValue(ItemsSourceProperty); 
            }

            set
            {
                if (!this.itemsIsDataBound && this.Items.Count > 0)
                {
                    throw new InvalidOperationException("Items collection must be empty before using ItemsSource.");
                }

                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        public ItemContainerGenerator ItemContainerGenerator 
        { 
            get; 
            private set; 
        }

        internal Panel Panel
        {
            get { return this.itemsPresenter == null ? null : this.itemsPresenter.Child; }
        }

        private DataTemplate DisplayMemberTemplate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.DisplayMemberPath) && this.displayMemberTemplate == null)
                {
                    MemoryStream s = new MemoryStream(Encoding.UTF8.GetBytes(@"
<DataTemplate xmlns=""https://github.com/grokys/Avalonia""
              xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
    <TextBlock Text=""{Binding " + this.DisplayMemberPath + @"}"" />
</DataTemplate>"));
                    this.displayMemberTemplate = (DataTemplate)XamlReader.Load(s);
                }

                return this.displayMemberTemplate;
            }
        }

        public static ItemsControl GetItemsOwner(DependencyObject element)
        {
            Panel panel = element as Panel;

            if (panel != null && panel.IsItemsHost)
            {
                ItemsPresenter itemsPresenter = VisualTreeHelper.GetParent(panel) as ItemsPresenter;

                if (itemsPresenter != null)
                {
                    return (ItemsControl)itemsPresenter.TemplatedParent;
                }
            }

            return null;
        }

        public static ItemsControl ItemsControlFromItemContainer(DependencyObject container)
        {
            var e = container as FrameworkElement;

            if (e != null)
            {
                var itctl = e.Parent as ItemsControl;

                if (itctl == null)
                {
                    return GetItemsOwner(e.Parent);
                }

                if (itctl.IsItemItsOwnContainer(e))
                {
                    return itctl;
                }
            }

            return null;
        }

        public override void OnApplyTemplate()
        {
            ItemsPresenter itemsPresenter =
                VisualTreeHelper.GetDescendents<ItemsPresenter>(this).FirstOrDefault();

            if (itemsPresenter != null)
            {
                itemsPresenter.Child = (Panel)this.ItemsPanel.CreateVisualTree(itemsPresenter);
                itemsPresenter.Child.IsItemsHost = true;
                this.SetItemsPresenter(itemsPresenter);
            }
        }

        internal void ClearContainerForItem(DependencyObject element, object item)
        {
            this.ClearContainerForItemOverride(element, item);
        }

        internal DependencyObject GetContainerForItem()
        {
            return this.GetContainerForItemOverride();
        }

        internal bool IsItemItsOwnContainer(object item)
        {
            return this.IsItemItsOwnContainerOverride(item);
        }

        internal virtual void OnItemsSourceChanged(IEnumerable oldSource, IEnumerable newSource)
        {
            if (newSource != null)
            {
                this.itemsIsDataBound = true;
                this.Items.SetSource(newSource);

                // Setting itemsIsDataBound to true prevents normal notifications from propagating, so do it manually here
                this.OnItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            else
            {
                this.itemsIsDataBound = false;
                this.Items.SetSource(null);
            }
        }

        internal virtual void OnItemTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            int count = this.Items.Count;

            for (int i = 0; i < count; i++)
            {
                this.UpdateContentTemplateOnContainer(
                    this.ItemContainerGenerator.ContainerFromIndex(i),
                    this.Items[i]);
            }
        }

        internal void PrepareContainerForItem(DependencyObject element, object item)
        {
            this.PrepareContainerForItemOverride(element, item);
        }

        internal void SetItemsPresenter(ItemsPresenter presenter)
        {
            if (this.itemsPresenter != null)
            {
                this.itemsPresenter.Child.InternalChildren.Clear();
            }

            this.itemsPresenter = presenter;
            this.AddItemsToPresenter(new GeneratorPosition(-1, 1), this.Items.Count);
        }

        protected virtual void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            // nothing to undo by default (since nothing was prepared)
        }

        protected virtual DependencyObject GetContainerForItemOverride()
        {
            return new ContentPresenter();
        }

        protected virtual bool IsItemItsOwnContainerOverride(object item)
        {
            return item is FrameworkElement;
        }

        protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
        }

        protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (this.DisplayMemberPath != null && this.ItemTemplate != null)
            {
                throw new InvalidOperationException("Cannot set 'DisplayMemberPath' and 'ItemTemplate' simultaneously.");
            }

            this.UpdateContentTemplateOnContainer(element, item);
        }

        private static void DisplayMemberPathChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ItemsControl)o).OnDisplayMemberPathChanged(
                e.OldValue as string,
                e.NewValue as string);
        }

        private static void ItemTemplateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ItemsControl)sender).OnItemTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void ItemsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ItemsControl)o).OnItemsSourceChanged(
                e.OldValue as IEnumerable,
                e.NewValue as IEnumerable);
        }

        private void InvokeItemsChanged(object o, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.SetLogicalParent(this, e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.SetLogicalParent(null, e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.SetLogicalParent(null, e.OldItems);
                    this.SetLogicalParent(this, e.NewItems);
                    break;
            }

            this.ItemContainerGenerator.OnOwnerItemsItemsChanged(o, e);

            if (!this.itemsIsDataBound)
            {
                this.OnItemsChanged(e);
            }
        }

        private void OnDisplayMemberPathChanged(string oldPath, string newPath)
        {
            // refresh the display member template.
            this.displayMemberTemplate = null;
            var newTemplate = this.DisplayMemberTemplate;

            int count = this.Items.Count;
            for (int i = 0; i < count; i++)
            {
                this.UpdateContentTemplateOnContainer(ItemContainerGenerator.ContainerFromIndex(i), this.Items[i]);
            }
        }

        private void OnItemContainerGeneratorChanged(object sender, ItemsChangedEventArgs e)
        {
            if (this.itemsPresenter == null || this.itemsPresenter.Child is VirtualizingPanel)
            {
                return;
            }

            Panel panel = this.itemsPresenter.Child;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    // the list has gone away, so clear the children of the panel
                    if (panel.InternalChildren.Count > 0)
                    {
                        this.RemoveItemsFromPresenter(new GeneratorPosition(0, 0), panel.InternalChildren.Count);
                    }

                    break;
                case NotifyCollectionChangedAction.Add:
                    this.AddItemsToPresenter(e.Position, e.ItemCount);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.RemoveItemsFromPresenter(e.Position, e.ItemCount);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.RemoveItemsFromPresenter(e.Position, e.ItemCount);
                    this.AddItemsToPresenter(e.Position, e.ItemCount);
                    break;
            }
        }

        private void SetLogicalParent(FrameworkElement parent, IList items)
        {
            foreach (DependencyObject o in items.OfType<DependencyObject>())
            {
                FrameworkElement p = LogicalTreeHelper.GetParent(o) as FrameworkElement;

                if (p != null)
                {
                    p.RemoveLogicalChild(o);
                }
            }

            if (parent != null)
            {
                foreach (object o in items)
                {
                    parent.AddLogicalChild(o);
                }
            }
        }

        private void AddItemsToPresenter(GeneratorPosition position, int count)
        {
            if (this.itemsPresenter == null ||
                this.itemsPresenter.Child == null ||
                this.itemsPresenter.Child is VirtualizingPanel)
            {
                return;
            }

            Panel panel = this.itemsPresenter.Child;
            int newIndex = this.ItemContainerGenerator.IndexFromGeneratorPosition(position);

            using (IDisposable p = ItemContainerGenerator.StartAt(position, GeneratorDirection.Forward, true))
            {
                for (int i = 0; i < count; i++)
                {
                    object item = this.Items[newIndex + i];
                    DependencyObject container = null;

                    bool fresh;
                    container = this.ItemContainerGenerator.GenerateNext(out fresh);

                    FrameworkElement f = container as FrameworkElement;

                    if (f != null && !(item is FrameworkElement))
                    {
                        f.DataContext = item;
                    }

                    panel.InternalChildren.Insert(newIndex + i, (UIElement)container);
                    this.ItemContainerGenerator.PrepareItemContainer(container);
                }
            }
        }

        private void RemoveItemsFromPresenter(GeneratorPosition position, int count)
        {
            if (this.itemsPresenter == null ||
                this.itemsPresenter.Child == null ||
                this.itemsPresenter.Child is VirtualizingPanel)
            {
                return;
            }

            Panel panel = this.itemsPresenter.Child;

            while (count-- > 0)
            {
                panel.InternalChildren.RemoveAt(position.Index);
            }
        }

        private void UpdateContentTemplateOnContainer(DependencyObject element, object item)
        {
            if (element == item)
            {
                return;
            }

            ContentPresenter presenter = element as ContentPresenter;
            ContentControl control = element as ContentControl;

            DataTemplate template = null;

            if (!(item is UIElement))
            {
                template = this.ItemTemplate;

                if (template == null)
                {
                    template = this.DisplayMemberTemplate;
                }
            }

            if (presenter != null)
            {
                presenter.ContentTemplate = template;
                presenter.Content = item;
            }
            else if (control != null)
            {
                control.ContentTemplate = template;
                control.Content = item;
            }
        }
    }
}
