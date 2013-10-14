using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Utils;

namespace Avalonia.Data
{
    class IndexedPropertyPathNode : PropertyPathNode, IListenCollectionChanged
    {
        static readonly PropertyInfo IListIndexer = GetIndexer(true, typeof(IList));

        bool isBroken;

        public object Index
        {
            get;
            private set;
        }

        IWeakListener Listener
        {
            get;
            set;
        }

        public IndexedPropertyPathNode(string index)
        {
            int val;
            if (int.TryParse(index, out val))
                Index = val;
            else
                Index = index;
        }

        void GetIndexer()
        {
            PropertyInfo = null;
            if (Source != null)
            {
                PropertyInfo = GetIndexer(Index is int, Source.GetType());
                if (PropertyInfo == null && Source is IList)
                    PropertyInfo = IListIndexer;
            }
        }

        static PropertyInfo GetIndexer(bool allowIntIndexer, Type type)
        {
            PropertyInfo propInfo = null;
            var members = type.GetDefaultMembers();
            foreach (PropertyInfo member in members)
            {
                var param = member.GetIndexParameters();
                if (param.Length == 1)
                {
                    if (allowIntIndexer && param[0].ParameterType == typeof(int))
                    {
                        propInfo = member;
                        break;
                    }
                    else if (param[0].ParameterType == typeof(string))
                    {
                        propInfo = member;
                    }
                }
            }

            return propInfo;
        }

        void IListenCollectionChanged.CollectionChanged(object o, NotifyCollectionChangedEventArgs e)
        {
            UpdateValue();
            if (Next != null)
                Next.SetSource(Value);
        }

        protected override void OnSourcePropertyChanged(object o, PropertyChangedEventArgs e)
        {
            UpdateValue();
            if (Next != null)
                Next.SetSource(Value);
        }

        protected override void OnSourceChanged(object oldSource, object newSource)
        {
            base.OnSourceChanged(oldSource, newSource);

            if (Listener != null)
            {
                Listener.Detach();
                Listener = null;
            }

            if (newSource is INotifyCollectionChanged)
                Listener = new WeakCollectionChangedListener((INotifyCollectionChanged)newSource, this);

            GetIndexer();
        }

        public override void SetValue(object value)
        {
            if (PropertyInfo != null)
                PropertyInfo.SetValue(Source, value, new object[] { Index });
        }

        protected override bool CheckIsBroken()
        {
            return isBroken || base.CheckIsBroken();
        }
        public override void UpdateValue()
        {
            if (PropertyInfo == null)
            {
                isBroken = true;
                ValueType = null;
                UpdateValueAndIsBroken(null, isBroken);
                return;
            }

            try
            {
                object newVal = PropertyInfo.GetValue(Source, new object[] { Index });
                isBroken = false;
                ValueType = PropertyInfo.PropertyType;
                UpdateValueAndIsBroken(newVal, isBroken);
            }
            catch
            {
                isBroken = true;
                ValueType = null;
                UpdateValueAndIsBroken(null, isBroken);
            }
        }
    }

}
