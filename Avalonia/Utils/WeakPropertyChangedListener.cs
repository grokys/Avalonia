using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Utils
{
    internal class WeakPropertyChangedListener : IWeakListener
    {
        WeakReference listener;

        IListenPropertyChanged Listener
        {
            get { return (IListenPropertyChanged)listener.Target; }
            set { listener = new WeakReference(value); }
        }

        DependencyProperty Property
        {
            get;
            set;
        }

        DependencyObject Source
        {
            get;
            set;
        }

        public WeakPropertyChangedListener(DependencyObject source, DependencyProperty property, IListenPropertyChanged listener)
        {
            Source = source;
            Property = property;
            Listener = listener;
            ((IObservableDependencyObject)Source).AttachPropertyChangedHandler(property.Name, OnPropertyChanged);
        }

        public void Detach()
        {
            if (Source != null)
            {
                ((IObservableDependencyObject)Source).RemovePropertyChangedHandler(Property.Name, OnPropertyChanged);
                Source = null;
            }
        }

        void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var l = Listener;
            if (l == null)
                Detach();
            else
                l.OnPropertyChanged(sender, e);
        }
    }
}
