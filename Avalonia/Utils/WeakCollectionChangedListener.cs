// -----------------------------------------------------------------------
// <copyright file="WeakCollectionChangedListener.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Utils
{
    using System;
    using System.Collections.Specialized;

    internal class WeakCollectionChangedListener : IWeakListener
    {
        private WeakReference listener;

        private INotifyCollectionChanged source;

        public WeakCollectionChangedListener(INotifyCollectionChanged source, IListenCollectionChanged listener)
        {
            this.source = source;
            this.Listener = listener;
            this.source.CollectionChanged += this.OnCollectionChanged;
        }

        private IListenCollectionChanged Listener
        {
            get { return (IListenCollectionChanged)this.listener.Target; }
            set { this.listener = new WeakReference(value); }
        }

        public void Detach()
        {
            this.source.CollectionChanged -= this.OnCollectionChanged;
            this.source = null;
        }

        private void OnCollectionChanged(object o, NotifyCollectionChangedEventArgs e)
        {
            var l = this.Listener;

            if (l == null)
            {
                this.source.CollectionChanged -= this.OnCollectionChanged;
            }
            else
            {
                l.CollectionChanged(o, e);
            }
        }
    }
}
