// -----------------------------------------------------------------------
// <copyright file="GroupDescription.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class GroupDescription : INotifyPropertyChanged
    {
        protected GroupDescription()
        {
            GroupNames = new ObservableCollection<object>();

            GroupNames.CollectionChanged += delegate
            {
                OnPropertyChanged(new PropertyChangedEventArgs("GroupNames"));
            };
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }

        protected virtual event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<object> GroupNames
        {
            get;
            private set;
        }

        public abstract object GroupNameFromItem(object item, int level, CultureInfo culture);

        public virtual bool NamesMatch(object groupName, object itemName)
        {
            return object.Equals(groupName, itemName);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeGroupNames()
        {
            return GroupNames.Count != 0;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var h = PropertyChanged;
            if (h != null)
                h(this, e);
        }
    }
}
