using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Utils
{
    abstract class WeakEventListener : IWeakListener
    {
        WeakReference listener;
        protected IListenEventRaised Listener
        {
            get { return (IListenEventRaised)listener.Target; }
            set { listener = new WeakReference(value); }
        }

        public WeakEventListener(IListenEventRaised listener)
        {
            Listener = listener;
        }

        public abstract void Detach();
    }
}
