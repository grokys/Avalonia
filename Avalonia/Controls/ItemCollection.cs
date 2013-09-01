// -----------------------------------------------------------------------
// <copyright file="ItemCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Data;

namespace Avalonia.Controls
{
    public sealed class ItemCollection : CollectionView, IList, IWeakEventListener
    {
        private List<object> items;

        public ItemCollection()
            : base(new List<object>())
        {
            this.items = (List<object>)this.SourceCollection;
        }

        public int Add(object value)
        {
            this.items.Add(value);
            return this.items.Count - 1;
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsSynchronized
        {
            get { return false; ; }
        }

        public object SyncRoot
        {
            get { return null; }
        }

        public object this[int index]
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public bool Contains(object value)
        {
            return this.items.Contains(value);
        }

        public int IndexOf(object value)
        {
            return this.items.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            this.items.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.items.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.items.RemoveAt(index);
        }

        public void CopyTo(Array array, int index)
        {
            this.items.CopyTo((object[])array, index);
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
