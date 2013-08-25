// -----------------------------------------------------------------------
// <copyright file="UIElementCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UIElementCollection : IList, ICollection, IEnumerable
    {
        private List<UIElement> items;

        private UIElement visualParent;
        
        private FrameworkElement logicalParent;

        public UIElementCollection(UIElement visualParent, FrameworkElement logicalParent)
        {
            this.items = new List<UIElement>();
            this.visualParent = visualParent;
            this.logicalParent = logicalParent;
        }

        public virtual int Capacity 
        {
            get { return items.Capacity; }
            set { items.Capacity = value; }
        }

        public virtual int Count 
        {
            get { return items.Count; }
        }
        
        public virtual bool IsSynchronized 
        {
            get { return false; }
        }
        
        public virtual object SyncRoot 
        {
            get { return null; }
        }

        public virtual UIElement this[int index] 
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }

        public virtual int Add(UIElement element)
        {
            this.items.Add(element);
            this.SetLogicalParent(element);
            return items.Count - 1;
        }
        
        public virtual void Clear()
        {
            this.items.Clear();
        }
        
        protected void ClearLogicalParent(UIElement element)
        {
            if (this.logicalParent != null)
            {
                this.logicalParent.RemoveLogicalChild(element);
            }

            if (this.visualParent != null)
            {
                this.visualParent.RemoveVisualChild(element);
            }
        }
        
        public virtual bool Contains(UIElement element)
        {
            return this.items.Contains(element);
        }
        
        public virtual void CopyTo(Array array, int index)
        {
            this.items.ToArray().CopyTo(array, index);
        }
        
        public virtual void CopyTo(UIElement[] array, int index)
        {
            this.items.CopyTo(array, index);
        }
        
        public virtual IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
        
        public virtual int IndexOf(UIElement element)
        {
            return this.items.IndexOf(element);
        }
        
        public virtual void Insert(int index, UIElement element)
        {
            this.items.Insert(index, element);
        }
        
        public virtual void Remove(UIElement element)
        {
            if (this.items.Remove(element))
            {
                this.ClearLogicalParent(element);
            }
        }
        
        public virtual void RemoveAt(int index)
        {
            UIElement element = this.items[index];
            this.items.RemoveAt(index);
            this.ClearLogicalParent(element);
        }
        
        public virtual void RemoveRange(int index, int count)
        {
            throw new NotImplementedException();
        }
        
        protected void SetLogicalParent(UIElement element)
        {
            if (this.logicalParent != null)
            {
                this.logicalParent.AddLogicalChild(element);
            }

            if (this.visualParent != null)
            {
                this.logicalParent.AddVisualChild(element);
            }
        }

        int IList.Add(object value)
        {
            return this.Add((UIElement)value);
        }

        void IList.Clear()
        {
            this.Clear();
        }

        bool IList.Contains(object value)
        {
            return this.Contains((UIElement)value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((UIElement)value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (UIElement)value);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            this.Remove((UIElement)value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (UIElement)value; }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return this.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return this.IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return this.SyncRoot; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
