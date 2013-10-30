// -----------------------------------------------------------------------
// <copyright file="ItemCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using Avalonia.Data;

    public sealed class ItemCollection : CollectionView
    {
        private ObservableCollection<object> internalList;

        internal ItemCollection()
            : base(new ObservableCollection<object>())
        {
            this.internalList = (ObservableCollection<object>)this.SourceCollection;
        }

        internal ItemCollection(IEnumerable source)
            : base(source)
        {
        }

        public override IEnumerable SourceCollection
        {
            get
            {
                return (this.internalList != null) ? this : base.SourceCollection;
            }
        }

        public Object this[int index] 
        {
            get 
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Add(object newItem)
        {
            this.CheckWritable();
            this.internalList.Add(newItem);
            return this.Count - 1;
        }

        internal new void SetSource(IEnumerable source)
        {
            if (source != null)
            {
                this.internalList = null;
                //base.SetSource(source);
            }
            else if (this.internalList == null)
            {
                this.internalList = new ObservableCollection<object>();
                //base.SetSource(this.internalList);
            }
        }

        private void CheckWritable()
        {
            if (this.internalList == null)
            {
                throw new InvalidOperationException(
                    "Cannot modify the Items collection while ItemsSource is set.");
            }
        }
    }
}
