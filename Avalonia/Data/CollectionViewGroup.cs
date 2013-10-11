using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public abstract class CollectionViewGroup : INotifyPropertyChanged
    {

        protected event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }

        public abstract bool IsBottomLevel
        {
            get;
        }

        public int ItemCount
        {
            get;
            private set;
        }

        public object Name
        {
            get;
            private set;
        }

        protected int ProtectedItemCount
        {
            get
            {
                return ItemCount;
            }
            set
            {
                if (ItemCount != value)
                {
                    ItemCount = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ItemCount"));
                }
            }
        }

        protected ObservableCollection<object> ProtectedItems
        {
            get;
            private set;
        }

        public ReadOnlyObservableCollection<object> Items
        {
            get;
            private set;
        }

        protected CollectionViewGroup(object name)
        {
            Name = name;
            ProtectedItems = new ObservableCollection<object>();
            Items = new ReadOnlyObservableCollection<object>(ProtectedItems);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var h = PropertyChanged;
            if (h != null)
                h(this, e);
        }
    }

}
