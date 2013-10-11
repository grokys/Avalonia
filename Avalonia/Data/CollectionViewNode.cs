using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Utils;

namespace Avalonia.Data
{
    internal class CollectionViewNode : PropertyPathNode, IListenEventRaised
    {
        bool BindsDirectlyToSource
        {
            get;
            set;
        }

        public bool BindToView
        {
            get;
            set;
        }

        PropertyChangedEventHandler ViewChangedHandler
        {
            get;
            set;
        }

        public CollectionViewNode(bool bindsDirectlyToSource, bool bindToView)
        {
            BindsDirectlyToSource = bindsDirectlyToSource;
            BindToView = bindToView;
            ViewChangedHandler = ViewChanged;
        }

        protected override void OnSourceChanged(object oldSource, object newSource)
        {
            CollectionViewSource source;
            ICollectionView view;
            base.OnSourceChanged(oldSource, newSource);

            source = oldSource as CollectionViewSource;
            view = oldSource as ICollectionView;
            if (source != null)
            {
                source.RemovePropertyChangedHandler(CollectionViewSource.ViewProperty, ViewChangedHandler);
                DisconnectViewHandlers(source.View);
            }
            else if (view != null)
            {
                DisconnectViewHandlers(view);
            }

            source = newSource as CollectionViewSource;
            view = newSource as ICollectionView;
            if (source != null)
            {
                source.AddPropertyChangedHandler(CollectionViewSource.ViewProperty, ViewChangedHandler);
                ConnectViewHandlers(source.View);
            }
            else if (view != null)
            {
                ConnectViewHandlers(view);
            }
        }

        void ConnectViewHandlers(ICollectionView view)
        {
            if (view != null)
                Listener = new WeakCurrentChangedListener(view, this);
        }

        void DisconnectViewHandlers(ICollectionView view)
        {
            if (Listener != null)
            {
                Listener.Detach();
                Listener = null;
            }
        }

        void ViewChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DisconnectViewHandlers((ICollectionView)e.OldValue);
            ConnectViewHandlers((ICollectionView)e.NewValue);
            ((IListenEventRaised)this).OnEventRaised(this, EventArgs.Empty);
        }

        void IListenEventRaised.OnEventRaised(object sender, EventArgs e)
        {
            UpdateValue();
            if (Next != null)
                Next.SetSource(Value);
        }

        public override void SetValue(object value)
        {
            throw new System.NotImplementedException();
        }

        protected override bool CheckIsBroken()
        {
            return Source == null;
        }

        public override void UpdateValue()
        {
            // The simple case - we use the source object directly, whatever it is.
            if (BindsDirectlyToSource)
            {
                ValueType = Source == null ? null : Source.GetType();
                UpdateValueAndIsBroken(Source, CheckIsBroken());
            }
            else
            {
                var usableSource = Source;
                ICollectionView view = null;
                if (Source is CollectionViewSource)
                {
                    usableSource = null;
                    view = ((CollectionViewSource)Source).View;
                }
                else if (Source is ICollectionView)
                {
                    view = (ICollectionView)Source;
                }

                // If we have no ICollectionView, then use the Source directly.
                if (view == null)
                {
                    ValueType = usableSource == null ? null : usableSource.GetType();
                    UpdateValueAndIsBroken(usableSource, CheckIsBroken());
                }
                else
                {
                    // If we have an ICollectionView and the property we're binding to exists
                    // on ICollectionView, we bind to the view. Otherwise we bind to its CurrentItem.
                    if (BindToView)
                    {
                        ValueType = view.GetType();
                        UpdateValueAndIsBroken(view, CheckIsBroken());
                    }
                    else
                    {
                        ValueType = view.CurrentItem == null ? null : view.CurrentItem.GetType();
                        UpdateValueAndIsBroken(view.CurrentItem, CheckIsBroken());
                    }
                }
            }
        }
    }
}
