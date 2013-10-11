using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Utils;

namespace Avalonia.Data
{
    class StandardPropertyPathNode : PropertyPathNode, IListenPropertyChanged
    {
        WeakPropertyChangedListener Listener
        {
            get;
            set;
        }

        public string PropertyName
        {
            get;
            private set;
        }

        public string TypeName
        {
            get;
            private set;
        }

        public StandardPropertyPathNode(string typeName, string propertyName)
        {
            TypeName = typeName;
            PropertyName = propertyName;
        }

        protected override void OnSourceChanged(object oldSource, object newSource)
        {
            base.OnSourceChanged(oldSource, newSource);

            var old_do = oldSource as DependencyObject;
            var new_do = newSource as DependencyObject;
            if (Listener != null)
            {
                Listener.Detach();
            }

            DependencyProperty = null;
            PropertyInfo = null;
            if (Source == null)
                return;

            var type = Source.GetType();

            if (TypeName != null)
                type = Type.GetType(TypeName);

            if (type == null)
                return;

            DependencyProperty prop;

            if (new_do != null && DependencyObject.TryGetPropertyFromName(type, PropertyName, out prop))
            {
                DependencyProperty = prop;
                Listener = new WeakPropertyChangedListener(new_do, DependencyProperty, this);
            }

            // If there's an attached DP called 'Foo' and also a regular CLR property
            // called 'Foo', we cannot use the property info from the CLR property.
            // Otherwise if the CLR property declares a type converter, we could
            // inadvertantly use it when we should not.
            if (DependencyProperty == null || !DependencyProperty.IsAttached)
            {
                PropertyInfo = type.GetProperty(PropertyName);
            }
        }

        void IListenPropertyChanged.OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                UpdateValue();
                if (Next != null)
                    Next.SetSource(Value);
            }
            catch (Exception ex)
            {
                try
                {
                    Console.WriteLine("Unhandled exception in StandardPropertyPathNode.DBChanged: {0}", ex);
                }
                catch
                {
                    // Ignore
                }
            }
        }

        protected override void OnSourcePropertyChanged(object o, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyName && PropertyInfo != null)
            {
                UpdateValue();
                if (Next != null)
                    Next.SetSource(Value);
            }
        }

        public override void SetValue(object value)
        {
            if (DependencyProperty != null)
                ((DependencyObject)Source).SetValue(DependencyProperty, value);
            else if (PropertyInfo != null)
                PropertyInfo.SetValue(Source, value, null);
        }

        public override void UpdateValue()
        {
            if (DependencyProperty != null)
            {
                ValueType = DependencyProperty.PropertyType;
                UpdateValueAndIsBroken(((DependencyObject)Source).GetValue(DependencyProperty), CheckIsBroken());
            }
            else if (PropertyInfo != null)
            {
                ValueType = PropertyInfo.PropertyType;
                try
                {
                    UpdateValueAndIsBroken(PropertyInfo.GetValue(Source, null), CheckIsBroken());
                }
                catch
                {
                    UpdateValueAndIsBroken(null, CheckIsBroken());
                }
            }
            else
            {
                ValueType = null;
                UpdateValueAndIsBroken(null, CheckIsBroken());
            }
        }
    }
}
