namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Utils;

    internal enum PropertyNodeType
    {
        AttachedProperty,
        Property,
        Indexed,
        None,
    }

    internal abstract class PropertyPathNode : IPropertyPathNode, IListenINPC
    {
        public event EventHandler IsBrokenChanged;
        public event EventHandler ValueChanged;

        public DependencyProperty DependencyProperty
        {
            get;
            protected set;
        }

        public bool IsBroken
        {
            get;
            private set;
        }

        public IWeakListener Listener
        {
            get;
            set;
        }

        public IPropertyPathNode Next
        {
            get;
            set;
        }

        public PropertyInfo PropertyInfo
        {
            get;
            protected set;
        }

        public object Source
        {
            get;
            set;
        }

        public object Value
        {
            get;
            private set;
        }

        public Type ValueType
        {
            get;
            protected set;
        }

        protected PropertyPathNode()
        {
            IsBroken = true;
        }

        protected virtual void OnSourceChanged(object oldSource, object newSource)
        {

        }

        protected virtual void OnSourcePropertyChanged(object o, PropertyChangedEventArgs e)
        {

        }

        public abstract void SetValue(object value);

        public void SetSource(object source)
        {
            if (Source != source || source == null)
            {
                var oldSource = Source;
                if (Listener != null)
                {
                    Listener.Detach();
                    Listener = null;
                }

                Source = source;
                if (Source is INotifyPropertyChanged)
                    Listener = new WeakINPCListener((INotifyPropertyChanged)Source, this);

                OnSourceChanged(oldSource, Source);
                UpdateValue();
                if (Next != null)
                    Next.SetSource(Value);
            }
        }

        public abstract void UpdateValue();

        protected virtual bool CheckIsBroken()
        {
            return Source == null || (PropertyInfo == null && DependencyProperty == null);
        }

        protected void UpdateValueAndIsBroken(object newValue, bool isBroken)
        {
            bool emitBrokenChanged = IsBroken != isBroken;
            bool emitValueChanged = !object.Equals(Value, newValue);

            IsBroken = isBroken;
            Value = newValue;

            // If Value changes it implicitly covers the case where
            // IsBroken changed too, so don't emit both events.
            if (emitValueChanged && ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
            else if (emitBrokenChanged && IsBrokenChanged != null)
            {
                IsBrokenChanged(this, EventArgs.Empty);
            }
        }

        void IListenINPC.OnPropertyChanged(object o, PropertyChangedEventArgs e)
        {
            OnSourcePropertyChanged(o, e);
        }
    }
}
