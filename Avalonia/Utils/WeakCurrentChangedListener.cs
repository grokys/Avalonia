using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data;

namespace Avalonia.Utils
{
    class WeakCurrentChangedListener : WeakEventListener
    {
        ICollectionView Source
        {
            get;
            set;
        }

        public WeakCurrentChangedListener(ICollectionView source, IListenEventRaised listener)
            : base(listener)
        {
            Source = source;
            source.CurrentChanged += HandleSourceCurrentChanged;
        }

        public override void Detach()
        {
            Source.CurrentChanged -= HandleSourceCurrentChanged;
        }

        void HandleSourceCurrentChanged(object sender, EventArgs e)
        {
            var listener = Listener;
            if (listener == null)
                Detach();
            else
                listener.OnEventRaised(sender, e);
        }
    }
}
