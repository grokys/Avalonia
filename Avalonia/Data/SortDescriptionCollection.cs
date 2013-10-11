using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public class SortDescriptionCollection : Collection<SortDescription>, INotifyCollectionChanged
    {
        public static readonly SortDescriptionCollection Empty = new SortDescriptionCollection();

        public SortDescriptionCollection()
        {
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add { CollectionChanged += value; }
            remove { CollectionChanged -= value; }
        }

        protected event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void ClearItems()
        {
            base.ClearItems();

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        protected override void InsertItem(int index, SortDescription item)
        {
            item.Seal();
            base.InsertItem(index, item);

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    item,
                    index));
            }
        }

        protected override void RemoveItem(int index)
        {
            SortDescription sd = base[index];
            base.RemoveItem(index);

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    sd,
                    index));
            }
        }

        protected override void SetItem(int index, SortDescription item)
        {
            SortDescription old = base[index];
            item.Seal();
            base.SetItem(index, item);

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    old,
                    index));
            }

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    item,
                    index));
            }
        }
    }
}
