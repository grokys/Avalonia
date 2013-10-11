// -----------------------------------------------------------------------
// <copyright file="WeakINPCListener.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Utils
{
    using System;
    using System.ComponentModel;

    internal class WeakINPCListener : IWeakListener
    {
        WeakReference listener;

        IListenINPC Listener
        {
            get { return (IListenINPC)listener.Target; }
            set { listener = new WeakReference(value); }
        }

        INotifyPropertyChanged Source
        {
            get;
            set;
        }

        public WeakINPCListener(INotifyPropertyChanged source, IListenINPC listener)
        {
            Source = source;
            Listener = listener;

            Source.PropertyChanged += HandleSourcePropertyChanged;
        }

        void HandleSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var listener = Listener;
            if (listener == null)
                Detach();
            else
                listener.OnPropertyChanged(sender, e);
        }

        public void Detach()
        {
            Source.PropertyChanged -= HandleSourcePropertyChanged; ;
        }
    }
}
